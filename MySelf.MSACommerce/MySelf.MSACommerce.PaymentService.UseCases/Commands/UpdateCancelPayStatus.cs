using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.PaymentService.Core.Enmus;
using MySelf.MSACommerce.PaymentService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Commands
{
    public record UpdateCancelPayStatusCommand(long OrderId) : ICommand<Result>;

public class UpdateCancelPayStatusCommandHandler(PaymentDbContext dbContext, ICapPublisher capPublisher) : ICommandHandler<UpdateCancelPayStatusCommand, Result>
    {
        public async Task<Result> Handle(UpdateCancelPayStatusCommand request, CancellationToken cancellationToken)
        {
            var payLog = await dbContext.PayLogs.FirstOrDefaultAsync(p => p.OrderId == request.OrderId,
                cancellationToken: cancellationToken);

            if (payLog == null) return Result.NotFound();

            payLog.Status = PayStatus.Cancel;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
