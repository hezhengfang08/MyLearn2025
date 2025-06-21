using MySelf.MSACommerce.SeckillService.Core.Enums;
using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.Core.Entities
{
    public class SeckillOrder:BaseAuditEntity
    {
        public long SeckillId { get; set; }
        public long Price {  get; set; }    
        public long UserId { get; set; }    
        public DateTime? PayTime { get; set; }
        public OrderStatus Status { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverMobile { get; set; }
        public string Receiver { get; set; }
        public string TransactionId { get; set; }

    }
}
