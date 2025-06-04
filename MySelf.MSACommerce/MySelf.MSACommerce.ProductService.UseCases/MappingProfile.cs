using MySelf.MSACommerce.ProductService.Core.Entities;
using MySelf.MSACommerce.ProductService.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Spu, SpuDto>();
        CreateMap<SpuDetail, SpuDetailDto>();
        CreateMap<Sku, SkuDto>();
    }
}

