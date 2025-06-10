using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.Core.Entities
{
    public class Cart(long userId)
    {
        public long UserId { get; set; } = userId;
        private readonly Dictionary<long, CartItem> _items = new();
        public IReadOnlyCollection<CartItem> _Items => _items.Values;
        public void AddOrUpdateItem(CartItem item) 
        { 
           if(!_items.TryAdd(item.SkuId, item))
            {
                _items[item.SkuId].UpdateQuantity(_items[item.SkuId].Quantity+  item.Quantity);
            }
        }
        public void RemoveItem(int skuId)
        {
            _items.Remove(skuId);
        }
        public void ClearCart()
        {
            _items.Clear();
        }
    }
}
