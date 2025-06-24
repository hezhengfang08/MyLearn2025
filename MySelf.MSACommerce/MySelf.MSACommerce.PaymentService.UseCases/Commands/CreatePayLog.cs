using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.PaymentService.Core.Enmus;
using MySelf.MSACommerce.PaymentService.Core.Entities;
using MySelf.MSACommerce.PaymentService.Infrastructure.Data;
using MySelf.MSACommerce.PaymentService.UseCases.Apis;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Commands
{
    public record CreatePayLogCommand(long OrderId) : ICommand<Result<long>>;

    public class CreatePayLogCommandHandler(PaymentDbContext dbContext,
        IServiceClient<IOrderServiceApi> orderService,
        IUser user) : ICommandHandler<CreatePayLogCommand, Result<long>>
    {
        public async Task<Result<long>> Handle(CreatePayLogCommand request, CancellationToken cancellationToken)
        {
            var payLog = await dbContext.PayLogs.FirstOrDefaultAsync(x => x.OrderId == request.OrderId,
                cancellationToken: cancellationToken);

            if (payLog != null)
            {
                if (payLog.Status == PayStatus.UnPay)
                {
                    return Result.Success(payLog.Id);
                }
                if (payLog.Status == PayStatus.Cancel)
                {
                    return Result.Failure("订单已取消");
                }
                if (payLog.Status == PayStatus.Payed)
                {
                    return Result.Failure("订单已支付");
                }
            }

            var response = await orderService.ServiceApi.GetOrderAsync(request.OrderId);

            if (!response.IsSuccessStatusCode) return Result.Failure("订单不存在");

            var order = response.Content!;

            payLog = new PayLog(request.OrderId, order.ActualPay, user.Id);

            dbContext.PayLogs.Add(payLog);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(payLog.Id);
        }
    }
}
