using DotNetCore.CAP;
using MediatR;
using MySelf.MSACommerce.PaymentService.UseCases.Commands;
using MySelf.MSACommerce.SharedEvent.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.CapSubscribes
{
    public class OrderSubscriber(ISender sender) : IOrderSubscriber, ICapSubscribe
    {
        [CapSubscribe(nameof(OrderCanceledEvent), Group = nameof(PaymentService))]
        public async Task OrderCanceledReceive(OrderCreatedEvent @event)
        {
            await sender.Send(new UpdateCancelPayStatusCommand(@event.OrderId));
        }
    }
}
