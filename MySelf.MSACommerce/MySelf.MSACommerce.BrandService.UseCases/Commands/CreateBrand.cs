using MySelf.MSACommerce.BrandService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace MySelf.MSACommerce.BrandService.UseCases.Commands
{
    public record CreateBrandCommand(string Name, string Image, string Letter) : ICommand<Result>;
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(query => query.Name)
                  .NotEmpty();
            RuleFor(query => query.Letter)
                .NotEmpty();
        }
    }
    public class CreateBrandCommandHandler(BrandDbContext dbContext, IFusionCache cache, IMapper mapper)
        : ICommandHandler<CreateBrandCommand, Result>
    {
        public async Task<Result> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = mapper.Map<Brand>(request);
            dbContext.Add(brand);
            var count = await dbContext.SaveChangesAsync(cancellationToken);
            if (count <= 0) return Result.Failure();
            // 删除缓存
            await cache.RemoveAsync(nameof(Brand), token: cancellationToken);
            return Result.Success();
        }
    }
}
