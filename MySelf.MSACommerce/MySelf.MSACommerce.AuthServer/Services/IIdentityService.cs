using MySelf.MSACommerce.SharedKernel.Result;

namespace MySelf.MSACommerce.AuthServer.Services
{
    public interface IIdentityService
    {
        Task<Result<string>> GetAccessTokenAsync(string userName, string password);
    }
}
