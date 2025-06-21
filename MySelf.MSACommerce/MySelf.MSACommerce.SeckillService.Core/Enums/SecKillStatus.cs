using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.Core.Enums
{
    public enum SecKillStatus
    {
        Queuing = 1,
        UnPayment = 2,
        Timeout = 3,
        Failed = 4,
        Completed = 5
    }
}
