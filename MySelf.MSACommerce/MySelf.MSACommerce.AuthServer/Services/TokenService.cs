using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace MySelf.MSACommerce.AuthServer.Services
{
    public class TokenService(IOptions<JwtSettings> jwtSettings) : ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret));
            var signinCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
       jwtSettings.Value.Issuer,
       jwtSettings.Value.Audience,
       claims: claims,
       expires: DateTime.Now.AddMinutes(jwtSettings.Value.AccessTokenExpirationMinutes),
       signingCredentials: signinCredentials
   );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber =new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParamter = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Value.Issuer,
                ValidAudience = jwtSettings.Value.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret))

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParamter, out var validatedToken);
            var jwtSecurityToken = validatedToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token无效");
            return principal;

        }
    }
}
