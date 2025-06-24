using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.PaymentService.Core.Enmus;
using MySelf.MSACommerce.PaymentService.Infrastructure.Data;
using MySelf.MSACommerce.SharedEvent.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Commands
{
    public record UpdatePayedStatusCommand(long Id) : ICommand<Result>;

    public class UpdatePayedStatusCommandHandler(PaymentDbContext dbContext, ICapPublisher capPublisher) : ICommandHandler<UpdatePayedStatusCommand, Result>
    {
        public async Task<Result> Handle(UpdatePayedStatusCommand request, CancellationToken cancellationToken)
        {
            var payLog = await dbContext.PayLogs.FirstOrDefaultAsync(p => p.Id == request.Id,
                cancellationToken: cancellationToken);

            if (payLog == null) return Result.NotFound();

            payLog.Status = PayStatus.Payed;

            await using var trans = await dbContext.Database.BeginTransactionAsync(capPublisher, cancellationToken: cancellationToken);
            payLog.PayTime = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);

            var orderPayedEvent = new OrderPayedEvent()
            {
                OrderId = payLog.OrderId
            };

            await capPublisher.PublishAsync(nameof(OrderPayedEvent), orderPayedEvent, cancellationToken: cancellationToken);

            await trans.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
