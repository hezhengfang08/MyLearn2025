using MySelf.MSACommerce.CategoryService.Core.Entities;
using MySelf.MSACommerce.CategoryService.UseCases.Commands;

namespace MySelf.MSACommerce.CategoryService.UseCases
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
        }
    }
}
