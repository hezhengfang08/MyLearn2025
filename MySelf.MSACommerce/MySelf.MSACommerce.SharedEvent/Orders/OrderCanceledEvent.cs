using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedEvent.Orders
{
    public record OrderCanceledEvent
    {
        public long OrderId { get; set; }
    }
}
