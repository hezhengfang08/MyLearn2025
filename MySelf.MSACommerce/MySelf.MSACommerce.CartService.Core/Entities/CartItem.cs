using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.Core.Entities
{
    public class CartItem(long skuId, string name, int quantity)
    {
        public long SkuId { get; set; } = skuId;
        public string Name { get; set; } = name;
        public int Quantity { get; set; } = quantity;
        public string? Image {  get; set; }
        public decimal Price { get; set; }
        public string? Spec {  get; set; }  
        public void UpdateQuantity(int quantity)
        {
            this.Quantity = quantity;
        }
    }
}
