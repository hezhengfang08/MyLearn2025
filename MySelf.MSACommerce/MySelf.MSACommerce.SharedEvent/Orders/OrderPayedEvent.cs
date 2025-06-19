using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedEvent.Orders
{
    public record OrderPayedEvent
    {
        public long OrderId { get; set; }
    };
}
