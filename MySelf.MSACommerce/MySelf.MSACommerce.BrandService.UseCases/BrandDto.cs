using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.BrandService.UseCases
{
   public record BrandDto(long Id,string Name, string? Image,string Litter);
}
