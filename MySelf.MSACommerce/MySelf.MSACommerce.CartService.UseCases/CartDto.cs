using MySelf.MSACommerce.CartService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.UseCases
{
    public class CartDto
    {
        public long UserId { get; set; }
        public IReadOnlyCollection<CartItem> Items { get; set; }
    }
    public class CartItemDto(long SkuId, string Name, int Quantity,string? Image, decimal Price,string? Spec);
}
