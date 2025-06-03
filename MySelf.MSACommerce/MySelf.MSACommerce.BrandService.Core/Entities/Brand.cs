using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.BrandService.Core.Entities
{
    public class Brand:BaseAuditEntity,IAggregateRoot
    {
        public string Name { get; set; }    
        public string? Image { get; set; }
        public string Letter { get; set; }
    }
}
