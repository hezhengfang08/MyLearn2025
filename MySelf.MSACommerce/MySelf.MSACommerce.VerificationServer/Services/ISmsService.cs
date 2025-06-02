using MySelf.MSACommerce.SharedKernel.Result;

namespace MySelf.MSACommerce.VerificationServer.Services
{
    public interface ISmsService
    {
        Task<Result> SendCodeAsync(string phoneNumber);
        Task<Result> VerifyCodeAsync(string phoneNumber, string inputCode);
    }
}
