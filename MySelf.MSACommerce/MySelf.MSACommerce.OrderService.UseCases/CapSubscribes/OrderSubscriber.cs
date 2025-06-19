using DotNetCore.CAP;
using MediatR;
using MySelf.MSACommerce.OrderService.Core.Enums;
using MySelf.MSACommerce.OrderService.UseCases.Commands;
using MySelf.MSACommerce.OrderService.UseCases.Queries;
using MySelf.MSACommerce.SharedEvent.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.CapSubscribes
{
    public class OrderSubscriber(ISender sender, ICapPublisher capPublisher) : IOrderSubscriber, ICapSubscribe
    {
        [CapSubscribe(nameof(OrderCreatedEventResult), Group = nameof(OrderService))]
        public Task OrderCreatedResultReceive(OrderCreatedEventResult? result)
        {
            if (result is null) return Task.CompletedTask;

            foreach (var failSku in result.ResvFailSkus)
            {
                Console.WriteLine($"库存不足，商品：{failSku.SkuId}，数量：{failSku.Quantity}");
            }
            return Task.CompletedTask;
        }

        [CapSubscribe(nameof(OrderPayedEvent), Group = nameof(OrderService))]
        public async Task OrderPayedReceive(OrderPayedEvent @event)
        {
            await sender.Send(new UpdateOrderStatusCommand(@event.OrderId, OrderStatus.Payed));
        }

        [CapSubscribe(nameof(OrderTimeoutEvent), Group = nameof(OrderService))]
        public async Task OrderTimeoutReceive(OrderTimeoutEvent @event)
        {
            var result = await sender.Send(new GetOrderStatusQuery(@event.OrderId));

            if (!result.IsSuccess || result.Value is null) return;

            if (result.Value.Status == OrderStatus.UnPayed)
            {
                await sender.Send(new UpdateOrderStatusCommand(@event.OrderId, OrderStatus.Canceled));
            }
        }
    }
}
