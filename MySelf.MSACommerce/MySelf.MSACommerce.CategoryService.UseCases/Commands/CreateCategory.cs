


using MySelf.MSACommerce.CategoryService.Core.Entities;
using ZiggyCreatures.Caching.Fusion;

namespace MySelf.MSACommerce.CategoryService.UseCases.Commands
{
    public record CreateCategoryCommand(string Name, long ParentId, bool isParent, int Sort) : ICommand<Result>;
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(query => query.Name)
               .NotEmpty();
        }
    }
    public class CreateCategoryCommandHandler(CategoryDbContext dbContext, IFusionCache fusionCache, IMapper mapper)
        : ICommandHandler<CreateCategoryCommand, Result>
    {
        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(request);
            dbContext.Add(category);
            var count = await dbContext.SaveChangesAsync(cancellationToken);
            if (count <= 0) return Result.Failure();
            // 清空缓存
            await fusionCache.RemoveAsync(nameof(Category), token: cancellationToken);
            return Result.Success();
        }
    }
}