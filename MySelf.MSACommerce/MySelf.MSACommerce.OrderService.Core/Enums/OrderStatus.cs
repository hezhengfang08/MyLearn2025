using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.Core.Enums
{
    public enum OrderStatus
    {
        UnPayed = 1,
        Payed = 2,
        UnShipped = 3,
        Shipped = 4,
        Completed = 5,
        Canceled = 6
    }
}
