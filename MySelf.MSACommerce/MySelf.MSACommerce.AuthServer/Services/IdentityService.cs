using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySelf.MSACommerce.SharedKernel.Result;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MySelf.MSACommerce.AuthServer.Services
{
    public class IdentityService(IUserService userService, IOptions<JwtSettings> jwtSettings) : IIdentityService
    {
        public async Task<Result<string>> GetAccessTokenAsync(string userName, string password)
        {
            var response = await userService.GetUserAsync(userName, password);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure("用户名或密码不正确");
            }
            var user = response.Content;
            var jwt = new JwtSecurityToken(
                jwtSettings.Value.Issuer,
                 jwtSettings.Value.Audience,
                 new[]
                 {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.MobilePhone, user.Phone ?? string.Empty)
                 },
                  expires: DateTime.Now.AddMinutes(jwtSettings.Value.AccessTokenExpirationMinutes),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret!)),
                SecurityAlgorithms.HmacSha256)

                );
            // 生成 JWT 字符串
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token is null ? Result.Failure() : Result.Success(token);
        }
    }
}
