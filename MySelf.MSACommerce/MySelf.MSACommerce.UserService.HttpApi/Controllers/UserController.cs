using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using MySelf.MSACommerce.UserService.HttpApi.Apis;
using MySelf.MSACommerce.UserService.HttpApi.Models;
using MySelf.MSACommerce.UserService.UseCases.Commands;
using MySelf.MSACommerce.UserService.UseCases.Queries;

namespace MySelf.MSACommerce.UserService.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IServiceClient<IVerificationApi> client) : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserQuery request)
        {
            var result = await Sender.Send(request);
            return ReturnResult(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            var response = await client.ServiceApi.VerifySmsCodeAsync(userDto.Phone, userDto.Code);
           if (!response.IsSuccessStatusCode) return BadRequest(response.Error.Content);

            var result = await Sender.Send(new CreateUserCommand(userDto.Username,userDto.Password, userDto.Phone));
            return ReturnResult(result);
        }
    }
}
