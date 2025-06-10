using MySelf.MSACommerce.CartService.Core.Data;
using MySelf.MSACommerce.CartService.Core.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.Infrastructure.Data
{
    public class RedisCartRepository(IDatabase database) : ICartRepository
    {
        public async Task AddOrUpdateItemAsync(long userId, CartItem item)
        {
            var cartKey = $"cart:{userId}";
            var itemJson = await database.HashGetAsync(cartKey, item.SkuId);

            if (itemJson.HasValue)
            {
                var itemDb = JsonSerializer.Deserialize<CartItem>(itemJson);
                item.UpdateQuantity(itemDb.Quantity + item.Quantity);
                item.Price = itemDb.Price;
            }
            var itemData = JsonSerializer.Serialize(item);
            await database.HashSetAsync(cartKey, item.SkuId, itemData);
        }

        public async Task ClearCartAsync(long userId)
        {
            var cartKey = $"cart:{userId}";
            await database.KeyDeleteAsync(cartKey);
        }

        public async Task<Cart?> GetCartAsync(long userId)
        {
            var cartKey = $"cart:{userId}";
            var entries = await database.HashGetAllAsync(cartKey);
            if(entries.Length == 0) { return null; }
            var cart = new Cart(userId);
            foreach (var entry in entries)
            {
                var item = JsonSerializer.Deserialize<CartItem>(entry.Value);
                cart.AddOrUpdateItem(item);
            }
            return cart;
        }

        public async Task RemoveItemAsync(long userId, long skuId)
        {
            var cartKey = $"cart:{userId}";
            await database.HashDeleteAsync(cartKey, skuId);
        }
    }
}
