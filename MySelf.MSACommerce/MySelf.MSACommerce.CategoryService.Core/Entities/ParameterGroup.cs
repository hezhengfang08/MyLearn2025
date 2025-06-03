using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CategoryService.Core.Entities
{
    public class ParameterGroup:BaseAuditEntity
    {
        public string Name { get; set; } = null!;
        public long CategoryId { get; set; }
        public ICollection<ParameterKey> ParameterKeys { get; set; }
    }
}
