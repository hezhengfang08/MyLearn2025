using MySelf.MSACommerce.ProductService.Core.Enums;
using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ProductService.Core.Entities
{
    public class Spu : BaseAuditEntity, IAggregateRoot
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public long CategoryId { get; set; }

        public long BrandId { get; set; }

        public Status Status { get; set; }

        public SpuDetail Detail { get; set; }

        public ICollection<Sku> Skus { get; set; }
    }
}
