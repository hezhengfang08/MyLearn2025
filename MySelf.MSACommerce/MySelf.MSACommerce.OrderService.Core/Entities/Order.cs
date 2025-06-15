using MySelf.MSACommerce.OrderService.Core.Enums;
using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.Core.Entities
{
    public class Order : BaseAuditEntity
    {
        // 总金额
        public long TotalPay { get; set; }

        // 实付金额
        public long ActualPay { get; set; }

        // 付款方式
        public PaymentType PaymentType { get; set; }

        // 用户ID
        public long UserId { get; set; }

        // 收货地址
        public string ReceiverAddress { get; set; } = null!;

        // 收货人
        public string Receiver { get; set; } = null!;

        public OrderInfo OrderInfo { get; set; }

        public ICollection<OrderDetail> OrderDetails;

        protected Order()
        {

        }

        public Order(long orderId)
        {
            Id = orderId;
            OrderInfo = new OrderInfo
            {
                Status = OrderStatus.UnPayed,
                CreateTime = DateTime.Now
            };

            OrderDetails = new List<OrderDetail>();
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            OrderDetails.Add(orderDetail);
            TotalPay += orderDetail.Price * orderDetail.Quantity;
        }
    }
}
