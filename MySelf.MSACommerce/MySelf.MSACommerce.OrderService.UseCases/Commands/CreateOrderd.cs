﻿using DotNetCore.CAP;
using IdGen;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.OrderService.Core.Entities;
using MySelf.MSACommerce.OrderService.UseCases.Apis;
using MySelf.MSACommerce.SharedEvent.Orders;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.Commands
{
    public record CreateOrderCommand(OrderForCreateDto Order) : ICommand<Result<long>>;

    public class CreateOrderCommandValidator : AbstractValidator<OrderForCreateDto>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(query => query.Carts)
                .Must(x => x.Count > 0);
        }
    }

    public class CreateOrderCommandHandler(OrderDbContext dbContext, IIdGenerator<long> idGen, IUser user,
        IServiceClient<IProductServiceApi> productClient,
        ICapPublisher capPublisher) : ICommandHandler<CreateOrderCommand, Result<long>>
    {
        public async Task<Result<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(idGen.CreateId())
            {
                UserId = user.Id,
                Receiver = "张三",
                ReceiverAddress = "朝夕",
                PaymentType = request.Order.PaymentType
            };

            var skuNum = request.Order.Carts.ToDictionary(m => m.SkuId, m => m.Quantity);

            var skusResponse = await productClient.ServiceApi.GetSkuListAsync(skuNum.Keys.ToArray());

            if (skusResponse.StatusCode == HttpStatusCode.NotFound) return Result.NotFound("商品信息不存在");

            if (!skusResponse.IsSuccessStatusCode) return Result.Failure("商品信息获取失败");

            var skus = skusResponse.Content;

            foreach (var sku in skus)
            {
                order.AddOrderDetail(new OrderDetail
                {
                    SkuId = sku.Id,
                    Name = sku.Name,
                    Quantity = skuNum[sku.Id],
                    Price = sku.Price,
                    Spec = sku.Spec.ToString(),
                    Image = sku.Images,
                });
            }

            // 模拟打折金额
            var PostFee = 1;
            // 计算实付金额
            order.ActualPay = order.TotalPay - PostFee;

            await using (var trans = await dbContext.Database.BeginTransactionAsync(capPublisher, cancellationToken: cancellationToken))
            {
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync(cancellationToken);

                var orderCreatedEvent = new OrderCreatedEvent()
                {
                    OrderId = order.Id,
                    Skus = order.OrderDetails.Select(x => new OrderSku(x.SkuId, x.Quantity)).ToList()
                };

                await capPublisher.PublishAsync(nameof(OrderCreatedEvent), orderCreatedEvent, nameof(OrderCreatedEventResult), cancellationToken);
                await capPublisher.PublishDelayAsync(TimeSpan.FromSeconds(600), nameof(OrderTimeoutEvent), new OrderTimeoutEvent(order.Id), cancellationToken: cancellationToken);
                await trans.CommitAsync(cancellationToken);
            }

            return Result.Success(order.Id);
        }
    }

}
