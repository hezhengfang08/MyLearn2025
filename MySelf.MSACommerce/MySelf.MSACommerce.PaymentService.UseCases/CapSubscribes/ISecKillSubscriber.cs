using MySelf.MSACommerce.SharedEvent.SecKills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.CapSubscribes
{
    public interface ISecKillSubscriber
    {
        Task SecKillTimeoutReceive(SecKillTimeoutEvent @event);
    }
}
