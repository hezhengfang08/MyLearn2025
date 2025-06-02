using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CategoryService.Core.Entities
{
    public class CategoryBrands:BaseAuditEntity
    {
        public long CategoryId { get; set; }
        public long BrandId { get; set; }

        public List<Category> Categories { get; set; }
    }
}
