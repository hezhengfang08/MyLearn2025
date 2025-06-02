using Refit;

namespace MySelf.MSACommerce.UserService.HttpApi.Apis
{
    public interface IVerificationApi
    {
        [Post("/api/verification/sms/verify")]
        Task<ApiResponse<string[]>> VerifySmsCodeAsync(string phoneNumber, string code);
    }
}
