using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.PaymentService.Core.Enmus;
using MySelf.MSACommerce.PaymentService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Queries
{
    public record PayStatusDto(PayStatus Status);

    public record GetPayStatusQuery(long OrderId) : IQuery<Result<PayStatusDto>>;

    public class GetPayStatusQueryHandler(PaymentDbContext dbContext)
        : IQueryHandler<GetPayStatusQuery, Result<PayStatusDto>>
    {
        public async Task<Result<PayStatusDto>> Handle(GetPayStatusQuery request,
            CancellationToken cancellationToken)
        {
            var payStatus = await dbContext.PayLogs
                .Where(p => p.OrderId == request.OrderId)
                .Select(p => new PayStatusDto(p.Status))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return payStatus is null ? Result.NotFound() : Result.Success(payStatus);
        }
    }
}
