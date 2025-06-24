using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedEvent.SecKills
{
    public record SecKillTimeoutEvent(long UserId, long OrderId);
}
