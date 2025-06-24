using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases
{
    public record SecKillOrderDto
    {
        public required string Id { get; set; }
        // 实付金额
        public required string ActualPay { get; set; }
    }
}
