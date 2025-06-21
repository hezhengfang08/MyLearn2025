using IdGen;
using MediatR;
using MySelf.MSACommerce.SeckillService.Core;
using MySelf.MSACommerce.SeckillService.Core.Entities;
using MySelf.MSACommerce.SeckillService.Core.Enums;
using MySelf.MSACommerce.SeckillService.Infrastructure.Data;
using MySelf.MSACommerce.SeckillService.UseCases.Queries;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.UseCases
{
    public class MultiThreadingCreateOrder(IConnectionMultiplexer redis, SecKillDbContext dbContext, IIdGenerator<long> idGen, ISender sender)
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();

        /// <summary>
        /// 多线程下单操作[真正抢单过程处理]
        /// </summary>
        public void CreateOrder()
        {
            //开启线程 内部已经配置了线程池
            Task.Run(() =>
            {
                // 从排队List中获取排队的信息（左边存储，右边取）
                var secKillQueueValue = _redisDb.ListRightPop(RedisKeyConstants.SecKillOrderQueue);
                if (secKillQueueValue.IsNullOrEmpty) return;

                try
                {
                    // 从redis中取出obj并反序列化为SeckillStatus
                    var seckillStatus = JsonSerializer.Deserialize<SecKillQueue>(secKillQueueValue);
                    if (seckillStatus == null) return;

                    //获取排队信息
                    var time = seckillStatus.Time;
                    var id = seckillStatus.SecKillId;//秒杀商品的ID
                    var username = seckillStatus.Username;
                    var userId = seckillStatus.UserId;

                    //判断 先从队列中获取商品 ,如果能获取到,说明 有库存,如果获取不到,说明 没库存 卖完了 return.
                    // 多线程竞争库存（单线程队列）
                    var ele = _redisDb.ListRightPop($"{RedisKeyConstants.SecKillStockListPrefix}{id}");
                    if (ele.IsNull)
                    {
                        //卖完了
                        //清除排队状态标识  防止重复排队的key
                        _redisDb.HashDelete(RedisKeyConstants.SecKillOrderStatus, userId);
                        throw new Exception("秒杀活动结束!");
                    }

                    // 获取商品详情数据
                    var product = sender.Send(new GetSecKillProductByIdQuery(time, id)).Result.Value;
                    if (product is null) throw new Exception("秒杀商品已售罄!");

                    var seckillOrder = new SeckillOrder()
                    {
                        Id = idGen.CreateId(), // 通过雪花算法
                        SeckillId = id,
                        Price = product.Price,
                        UserId = userId,
                        Status = OrderStatus.UnPay
                    };
                    //将秒杀订单存入到Redis中
                    _redisDb.HashSet(RedisKeyConstants.SecKillOrder, userId, JsonSerializer.Serialize(seckillOrder));

                    //5.减库存
                    var stock = _redisDb.StringIncrement($"{RedisKeyConstants.SecKillStockPrefix}{id}", -1);
                    product.StockCount = stock;
                    product.Num = product.Num++;

                    //判断当前商品是否还有库存
                    if (product.StockCount <= 0)
                    {
                        //将商品数据同步到MySQL中
                        dbContext.SecKillProducts.Update(product);
                    }
                    else
                    {
                        //如果有库存，则将数据重置到Redis
                        _redisDb.HashSet($"{RedisKeyConstants.SeckillDatePrefix}{time}", id,
                            JsonSerializer.Serialize(seckillOrder));
                    }

                    // //7.模拟下单
                    // try
                    // {
                    //     Console.WriteLine("开始模拟下单操作=====start====" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Thread.CurrentThread.ManagedThreadId);
                    //     Thread.Sleep(10000);
                    //     Console.WriteLine("开始模拟下单操作=====end====" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Thread.CurrentThread.ManagedThreadId);
                    // }
                    // catch (Exception)
                    // {
                    //     Console.WriteLine("下单失败");
                    //     seckillStatus.Status = SecKillStatus.Failed;
                    //     _redisDb.HashSet(RedisKeyConstants.SecKillOrderStatus, userId, JsonSerializer.Serialize(seckillStatus));
                    //     return;
                    // }

                    // 修改抢购状态
                    // 抢单成功，更新抢单状态,排队->等待支付
                    seckillStatus.Status = SecKillStatus.UnPayment;
                    seckillStatus.OrderId = seckillOrder.Id;
                    seckillStatus.Price = seckillOrder.Price;
                    _redisDb.HashSet(RedisKeyConstants.SecKillOrderStatus, userId, JsonSerializer.Serialize(seckillStatus));

                    //TODO 发布超时订单处理的消息。 【超时支付订单逻辑完全一样】

                    Console.WriteLine($"秒杀库存剩余量为：{product.StockCount}");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("子线程已经执行完成");
            });
        }
    }
}
