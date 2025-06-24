using DotNetCore.CAP;
using MediatR;
using MySelf.MSACommerce.SharedEvent.SecKills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.CapSubscribes
{
    public class SecKillSubscriber(ISender sender) : ISecKillSubscriber, ICapSubscribe
    {
        [CapSubscribe(nameof(SecKillTimeoutEvent), Group = nameof(PaymentService))]
        public Task SecKillTimeoutReceive(SecKillTimeoutEvent @event)
        {
            // TODO 关闭支付
            Console.WriteLine($"秒杀超时{@event.UserId}-${@event.OrderId}");
            return Task.CompletedTask;
        }
    }
}
