using MySelf.MSACommerce.SearchService.Core.Entities;
using MySelf.MSACommerce.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SearchService.UseCases
{
    public record SearchProductDto
    {
        public List<BrandDto> Brands { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public PagedList<Product> Products { get; set; }
        public PagedMetaData Page { get; set; }
    }
}
