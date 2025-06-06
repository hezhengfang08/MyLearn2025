using MySelf.MSACommerce.ProductDetailPage.Models;
using Refit;

namespace MySelf.MSACommerce.ProductDetailPage.Apis
{
    public interface IProductServiceApi
    {
        [Get("/api/product/spu")]
        Task<ApiResponse<SpuDto>> GetSpuAsync(long id);
    }
}
