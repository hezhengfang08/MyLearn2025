using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedKernel.Domain
{
    public abstract class BaseAuditEntity:BaseEntity<long>
    {
        public DateTimeOffset? CreateAt { get; set; }
        public DateTimeOffset? LastModifiedAt { get;set; }
    }
}
