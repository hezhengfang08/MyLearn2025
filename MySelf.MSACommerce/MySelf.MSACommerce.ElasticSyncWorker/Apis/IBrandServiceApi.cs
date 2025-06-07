using MySelf.MSACommerce.ElasticSyncWorker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ElasticSyncWorker.Apis
{
    public interface IBrandServiceApi
    {
        [Get("/api/brand")]
        Task<ApiResponse<BrandDto>> GetBrandAsync(long id);
    }
}
