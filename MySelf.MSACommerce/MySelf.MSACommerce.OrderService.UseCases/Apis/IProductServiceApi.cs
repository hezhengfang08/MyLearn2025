using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.Apis
{
    public interface IProductServiceApi
    {
        [Get("/api/product/sku/list")]
        Task<ApiResponse<List<SkuDto>>> GetSkuListAsync([Body] long[] ids);
    }
}
