using System.Security.Claims;

namespace MySelf.MSACommerce.AuthServer.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
    }
}
