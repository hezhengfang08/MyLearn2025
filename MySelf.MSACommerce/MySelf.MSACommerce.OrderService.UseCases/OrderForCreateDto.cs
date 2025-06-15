using MySelf.MSACommerce.OrderService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases
{
    public record CartDto(long SkuId, int Quantity);

    public record OrderForCreateDto
    {
        public long AddressId { get; set; }

        public List<CartDto> Carts { get; set; }

        public PaymentType PaymentType { get; set; }
    };

}
