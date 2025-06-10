using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.CartService.UseCases.Commands;
using MySelf.MSACommerce.CartService.UseCases.Queries;
using MySelf.MSACommerce.CartService.UseCases;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;

namespace MySelf.MSACommerce.CartService.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController(IUser  user) : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var result = await Sender.Send(new GetCartQuery(user.Id));
            return ReturnResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var result = await Sender.Send(new DeleteCartCommand(user.Id));
            return ReturnResult(result);
        }

        [HttpPost("items")]
        [HttpPut("items")]
        public async Task<IActionResult> CreateOrUpdateItem([FromBody] CartItemDto item)
        {
            var result = await Sender.Send(new CreateOrUpdateItemCommand(user.Id, item));
            return ReturnResult(result);
        }

        [HttpDelete("items/{itemId:long}")]
        public async Task<IActionResult> DeleteItem(long itemId)
        {
            var result = await Sender.Send(new DeleteItemCommand(user.Id, itemId));
            return ReturnResult(result);
        }
    }
}
