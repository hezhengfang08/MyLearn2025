using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ProductService.UseCases.Queries
{
    public record GetSpuFullListQuery(Pagination Pagination) : IQuery<Result<PagedList<SpuDto>>>;
    public class GetSpuFullListQueryValidtor : AbstractValidator<GetSpuFullListQuery>
    {
        public GetSpuFullListQueryValidtor()
        {
            RuleFor(query => query.Pagination)
                .NotEmpty();
        }
    }
    public class GetSpuFullListQueryHandler(ProductDbContext dbContext, IMapper mapper) : IQueryHandler<GetSpuFullListQuery, Result<PagedList<SpuDto>>>
    {
        public async Task<Result<PagedList<SpuDto>>> Handle(GetSpuFullListQuery request, CancellationToken cancellationToken)
        {
            var queryInfo =  dbContext.Spus.AsNoTracking().Include(x => x.Detail)
            .Include(x => x.Skus)
            .OrderBy(x => x.Id);
            var count = queryInfo.Count();
            if (count == 0) return Result.NotFound();

            var spus = await queryInfo
            .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);

            var spusDto = mapper.Map<List<SpuDto>>(spus);
            var pagedList = new PagedList<SpuDto>(spusDto, count, request.Pagination);
            return Result.Success(pagedList);
        }
    }
}
