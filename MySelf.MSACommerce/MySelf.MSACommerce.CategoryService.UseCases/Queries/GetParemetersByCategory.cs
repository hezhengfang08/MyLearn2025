using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.CategoryService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace MySelf.MSACommerce.CategoryService.UseCases.Queries
{
    public record GetSpecsByCategoryQuery(long CategoryId) : IQuery<Result<List<SpecKeyDto>>>;
    public class GetSpecByCategoryQueryValidator : AbstractValidator<GetSpecsByCategoryQuery>
    {
        public GetSpecByCategoryQueryValidator()
        {
            RuleFor(query => query.CategoryId)
               .GreaterThan(0);
        }
    }
    public class GetSpecsByCategoryQueryHandler(CategoryDbContext dbContext, IFusionCache cache)
    : IQueryHandler<GetSpecsByCategoryQuery, Result<List<SpecKeyDto>>>
    {
        public async Task<Result<List<SpecKeyDto>>> Handle(GetSpecsByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            // 从缓存中获取规则参数
            var key = $"{nameof(SpecKey)}:{request.CategoryId}";
            var specKeysDto = await cache.GetOrSetAsync<List<SpecKeyDto>>(key,
                async token =>
                {
                    var specKeys = await dbContext.SpecKeys.AsNoTracking()
                        .Where(s => s.CategoryId == request.CategoryId)
                        .Select(s => new SpecKeyDto(s.Id, s.Name))
                        .ToListAsync(token);

                    return specKeys;
                },

                options => options.SetDurationInfinite(),
                token: cancellationToken);

            if (specKeysDto.Count == 0) return Result.NotFound();

            return Result.Success(specKeysDto);
        }

    }
}
