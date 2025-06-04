using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ProductService.Core.Entities
{
    public class SpuDetail : BaseAuditEntity
    {
        public string Introduction { get; set; } = null!;
        public string Spec { get; set; } = null!;
        public string Parameter { get; set; } = null!;
    }
}
