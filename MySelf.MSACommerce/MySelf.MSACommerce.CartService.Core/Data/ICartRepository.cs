using MySelf.MSACommerce.CartService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.Core.Data
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(long userId);
        Task AddOrUpdateItemAsync(long userId, CartItem item);
        Task RemoveItemAsync(long userId, long skuId);
        Task ClearCartAsync(long userId);
    }
}
