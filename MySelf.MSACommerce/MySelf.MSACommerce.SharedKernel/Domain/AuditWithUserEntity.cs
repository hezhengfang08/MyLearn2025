using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedKernel.Domain
{
    public abstract class AuditWithUserEntity: BaseAuditEntity
    {
        public long? CreatedBy { get; set; }
        public long? LastModifiedBy { get; set; }
    }
}
