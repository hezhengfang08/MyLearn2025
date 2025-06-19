using DotNetCore.CAP;
using MediatR;
using MySelf.MSACommerce.CartService.UseCases.Commands;
using MySelf.MSACommerce.SharedEvent.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.UseCases.CapSubscribes
{
    public class OrderSubscriber(ISender sender) : IOrderSubscriber, ICapSubscribe
    {
        [CapSubscribe(nameof(OrderCreatedEvent),Group =nameof(CartService))]
        public async Task OrderCreatedReceive(OrderCreatedEvent orderCreatedEvent)
        {
            foreach (var sku in orderCreatedEvent.Skus)
            {
                await sender.Send(new DeleteItemCommand(orderCreatedEvent.UserId, sku.SkuId));
            }
        }
    }
}
