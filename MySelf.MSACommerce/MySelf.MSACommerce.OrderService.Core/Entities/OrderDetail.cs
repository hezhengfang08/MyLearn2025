using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.Core.Entities
{
    public class OrderDetail:BaseEntity<long>
    {
        // 订单ID
        public long OrderId { get; set; }
        // 商品SKU
        public long SkuId { get; set; }
        // 数量
        public long Quantity { get; set; }
        // 商品名称
        public string Name { get; set; } = null!;
        // 规格
        public string Spec { get; set; }
        // 价格
        public long Price { get; set; }
        // 图片
        public string? Image { get; set; }

        // 订单
        public Order Order { get; set; }
    }
}
