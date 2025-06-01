using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Refit;

namespace MySelf.MSACommerce.AuthServer.Apis
{
    public record UserDto ( long Id,string UserName, string? Phone);
    public interface IUserServiceApi
    {
        [Get("/api/user")]
        Task<ApiResponse<UserDto>>GetUserAsync(string userName, string password);
    }
}
