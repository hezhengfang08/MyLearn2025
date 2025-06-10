using MySelf.MSACommerce.CartService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.UseCases
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemDto, CartItem>();
        }
    }
}
