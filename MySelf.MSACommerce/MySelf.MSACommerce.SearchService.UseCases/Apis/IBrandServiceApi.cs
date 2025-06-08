using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SearchService.UseCases.Apis
{
    public interface IBrandServiceApi
    {
        [Get("/api/brand/list")]
        Task<ApiResponse<List<BrandDto>>> GetBrandsAsync([Body] long[] ids);
    }
}
