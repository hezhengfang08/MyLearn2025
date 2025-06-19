using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.OrderService.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.Queries
{
    public record OrderStatusDto(long Id, OrderStatus Status);

    public record GetOrderStatusQuery(long OrderId) : IQuery<Result<OrderStatusDto>>;

    public class GetOrderStatusHandler(OrderDbContext dbContext, IMapper mapper) : IQueryHandler<GetOrderStatusQuery, Result<OrderStatusDto>>
    {
        public async Task<Result<OrderStatusDto>> Handle(GetOrderStatusQuery request, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders
                .Include(x => x.OrderInfo)
                .Where(o => o.Id == request.OrderId)
                .Select(o => new OrderStatusDto(request.OrderId, o.OrderInfo.Status))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return order == null ? Result.NotFound() : Result.Success(order);
        }
    }
}
