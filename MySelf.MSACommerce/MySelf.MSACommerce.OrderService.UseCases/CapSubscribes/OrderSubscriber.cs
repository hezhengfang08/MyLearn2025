using DotNetCore.CAP;
using MediatR;
using MySelf.MSACommerce.SharedEvent.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.CapSubscribes
{
    public class OrderSubscriber(ISender sender) : IOrderSubscriber, ICapSubscribe
    {
        [CapSubscribe(nameof(OrderCreatedEventResult))]
        public Task OrderCreatedResultReceive(OrderCreatedEventResult result)
        {
            foreach (var failSku in result.ResvFailSkus)
            {
                Console.WriteLine($"库存不足，商品：{failSku.SkuId}，数量：{failSku.Quantity}");
            }
            return Task.CompletedTask;
        }
    }
}
