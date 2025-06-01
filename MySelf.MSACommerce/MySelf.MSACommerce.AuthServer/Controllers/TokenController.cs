using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.AuthServer.Apis;
using MySelf.MSACommerce.AuthServer.Services;
using MySelf.MSACommerce.CommonServiceClient;
using StackExchange.Redis;
using System.Security.Claims;

namespace MySelf.MSACommerce.AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(ITokenService tokenService
        , IServiceClient<IUserServiceApi> serviceClient, IConnectionMultiplexer redis) : ControllerBase
    {
        private readonly IDatabase _redisDb = redis.GetDatabase();
        [HttpGet]
        public async Task<IActionResult> Get(string username, string password)
        {
            var response = await serviceClient.ServiceApi.GetUserAsync(username, password);
            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }
            var user = response.Content;
            // 生成用户声明
            var claims = new List<Claim>
               {
                   new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new(ClaimTypes.Name, user.UserName),
                   new(ClaimTypes.MobilePhone, user.Phone ?? "")
               };
            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();
            var key = $"user:refreshToken:{username}";
            await _redisDb.StringSetAsync(key, refreshToken, TimeSpan.FromDays(7), When.Always);
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
        [HttpPut("refresh")]
        public async Task<IActionResult> Refresh(string accessToken,string refreshToken)
        {
            var principal = tokenService.GetClaimsPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;
            //二次验证等
            var key = $"user:refreshToken:{userName}";
            var dbRefreshToken = await _redisDb.StringGetAsync(key);
            if (dbRefreshToken.IsNull || dbRefreshToken != refreshToken)
            {
                return BadRequest("无效的请求");
            }

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            await _redisDb.StringSetAsync(key,newRefreshToken,keepTtl:true);
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });

        }
        [HttpDelete, Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;
            var key = $"user:refreshToken:{username}";
            await _redisDb.KeyDeleteAsync(key);
            return NoContent();
        }
    }
}
