using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.StockService.Core.Entities
{
    public class SkuStock:BaseEntity<long>
    {
        public long TotalQty {  get; set; } 
        public long AvailQty { get; set; }
        public long ResvQty { get; set; }
        public ICollection<StockResv> StockResvs { get; set; } = new List<StockResv>();

        public void AddResvQty(long orderId, long qty, int exprMinutes)
        {
            AvailQty -= qty;
            ResvQty += qty;
            StockResvs.Add(new StockResv(orderId, qty, DateTime.Now.AddMinutes(exprMinutes)));
        }
    }
}
