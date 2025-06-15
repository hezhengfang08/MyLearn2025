using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedEvent.Orders
{
    public record OrderSku(long SkuId, long Quantity);

    public record OrderCreatedEvent
    {
        public long OrderId { get; set; }
        public List<OrderSku> Skus { get; set; }
    }

    public record OrderCreatedEventResult
    {
        public long OrderId { get; init; }
        public List<OrderSku> ResvFailSkus { get; init; }
    }
}
