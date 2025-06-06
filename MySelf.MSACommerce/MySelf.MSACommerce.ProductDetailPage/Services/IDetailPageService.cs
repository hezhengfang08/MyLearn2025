using MySelf.MSACommerce.SharedKernel.Result;

namespace MySelf.MSACommerce.ProductDetailPage.Services
{
    public interface IDetailPageService
    {
        Task<Result<Dictionary<string, object>>> GetSpuModel(long id);
    }
}
