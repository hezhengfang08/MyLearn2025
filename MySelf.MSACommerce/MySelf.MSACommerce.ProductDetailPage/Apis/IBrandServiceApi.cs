using MySelf.MSACommerce.ProductDetailPage.Models;
using Refit;

namespace MySelf.MSACommerce.ProductDetailPage.Apis
{
    public interface IBrandServiceApi
    {
        [Get("/api/brand")]
        Task<ApiResponse<BrandDto>> GetBrandAsync(long id);
    }
}
