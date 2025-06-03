using MySelf.MSACommerce.BrandService.Core.Entities;
using MySelf.MSACommerce.BrandService.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.BrandService.UseCases
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {

            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<Brand, BrandDto>();
        }
    }
}
