using MySelf.MSACommerce.ElasticSyncWorker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ElasticSyncWorker.Apis
{
    public interface ICategoryServiceApi
    {
        [Get("/api/category/parents")]
        Task<ApiResponse<List<CategoryDto>>> GetParents(long id);

        [Get("/api/category/specs")]
        Task<ApiResponse<List<SpecKeyDto>>> GetSpecs(long id);

        [Get("/api/category/parameters")]
        Task<ApiResponse<List<ParameterGroupDto>>> GetParameters(long id);
    }
}
