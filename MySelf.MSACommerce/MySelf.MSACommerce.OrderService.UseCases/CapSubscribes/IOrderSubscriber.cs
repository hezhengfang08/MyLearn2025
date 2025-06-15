using MySelf.MSACommerce.SharedEvent.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.CapSubscribes
{
    public interface IOrderSubscriber
    {
        Task OrderCreatedResultReceive(OrderCreatedEventResult result);
    }
}
