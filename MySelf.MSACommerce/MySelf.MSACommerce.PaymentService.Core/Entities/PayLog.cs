using MySelf.MSACommerce.PaymentService.Core.Enmus;
using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.Core.Entities
{
    public class PayLog:BaseAuditEntity
    {
        public long OrderId { get; set; }
        public long TotalFee { get; set; }
        public long UserId { get; set; }
        public PayStatus Status { get; set; }
        public DateTime? PayTime { get; set; }

        protected PayLog()
        {

        }

        public PayLog(long orderId, long totalFee, long userId)
        {
            OrderId = orderId;
            TotalFee = totalFee;
            UserId = userId;
            Status = PayStatus.UnPay;
        }
    }
}
