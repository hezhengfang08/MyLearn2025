using MySelf.MSACommerce.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CategoryService.Core.Entities
{
    public class ParameterKey:BaseAuditEntity
    {
        public string Name {  get; set; }
        public long ParameterGroupId { get; set; }    
        public ParameterGroup ParameterGroup { get; set; } 

        public long CategoryId { get; set; }
    }
}
