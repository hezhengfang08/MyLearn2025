using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Refit;

namespace MySelf.MSACommerce.AuthServer.Services
{
    public record UserDto ( long Id,string UserName, string? Phone);
    public interface IUserService
    {
        [Get("/api/user")]
        Task<ApiResponse<UserDto>>GetUserAsync(string UserName, string Password);
    }
}
