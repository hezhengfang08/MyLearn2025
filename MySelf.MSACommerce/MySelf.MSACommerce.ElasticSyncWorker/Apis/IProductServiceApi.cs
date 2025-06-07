using MySelf.MSACommerce.ElasticSyncWorker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ElasticSyncWorker.Apis
{
    public interface IProductServiceApi
    {
        [Get("/api/product/spu")]
        Task<ApiResponse<SpuDto>> GetSpuAsync(long id);

        [Get("/api/product/spu/list")]
        Task<ApiResponse<List<SpuDto>>> GetSpuListAsync(int pageNumber, int pageSize);
    }
}
