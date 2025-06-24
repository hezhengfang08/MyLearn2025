using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.PaymentService.Core.Enmus;
using MySelf.MSACommerce.PaymentService.Infrastructure.Data;
using MySelf.MSACommerce.SharedEvent.SecKills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Commands
{
    public record UpdateSecKillPayedStatusCommand(long Id) : ICommand<Result>;

    public class UpdateSecKillPayedStatusCommandHandler(PaymentDbContext dbContext, ICapPublisher capPublisher) : ICommandHandler<UpdateSecKillPayedStatusCommand, Result>
    {
        public async Task<Result> Handle(UpdateSecKillPayedStatusCommand request, CancellationToken cancellationToken)
        {
            var payLog = await dbContext.PayLogs.FirstOrDefaultAsync(p => p.Id == request.Id,
                cancellationToken: cancellationToken);

            if (payLog == null) return Result.NotFound();

            payLog.Status = PayStatus.Payed;

            await using var trans = await dbContext.Database.BeginTransactionAsync(capPublisher, cancellationToken: cancellationToken);
            payLog.PayTime = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);

            var secKillPayedEvent = new SecKillPayedEvent()
            {
                UserId = payLog.UserId
            };

            await capPublisher.PublishAsync(nameof(SecKillPayedEvent), secKillPayedEvent, cancellationToken: cancellationToken);

            await trans.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
