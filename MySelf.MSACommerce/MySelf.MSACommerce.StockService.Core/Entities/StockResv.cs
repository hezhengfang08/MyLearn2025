using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.StockService.Core.Entities
{
    public class StockResv : BaseEntity<long>
    {
        public long OrderId { get; set; }
        public long ResvQty { get; set; }
        public DateTime ExprTime { get; set; }
        public long SkuId { get; set; }

        public SkuStock SkuStock { get; set; }
        public StockResv(long orderId, long resvQty, DateTime exprTime)
        {
            OrderId = orderId;
            ResvQty = resvQty;
            ExprTime = exprTime;
        }

    }
}
