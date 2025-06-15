using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using MySelf.MSACommerce.OrderService.UseCases.Commands;
using MySelf.MSACommerce.OrderService.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace MySelf.MSACommerce.OrderService.HttpApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        [HttpPost]
        [PreventDuplicateRequestFilter]
        public async Task<IActionResult> CreateOrder(OrderForCreateDto createDto)
        {
            var result = await Sender.Send(new CreateOrderCommand(createDto));
            return ReturnResult(result);
        }
    }
}
