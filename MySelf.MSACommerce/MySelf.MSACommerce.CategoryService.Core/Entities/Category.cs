using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CategoryService.Core.Entities
{
    public class Category:BaseAuditEntity,IAggregateRoot
    {
        public string Name { get; set; } = null!;
        public long ParentId {  get; set; }
        public bool IsParent { get; set; }
        public int Sort { get; set; }

        public ICollection<CategoryBrands> Brands { get; set; }

    }
}
