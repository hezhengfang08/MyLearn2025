using MySelf.MSACommerce.ProductService.Core.Enums;
using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ProductService.Core.Entities
{
    public class Sku:BaseAuditEntity
    {
        public long SpuId { get; set; }

        public string Name { get; set; } = null!;
        public string? Images {  get; set; }
        public long Price { get; set; }
        public string Indexes { get; set; } = null!;

        public string Spec {  get; set; } = null!;
        public Status Status { get; set; }

    }
}
