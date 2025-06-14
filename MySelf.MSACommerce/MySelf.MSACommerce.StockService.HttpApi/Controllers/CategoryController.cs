using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using MySelf.MSACommerce.StockService.UseCases.Commands;

namespace MySelf.MSACommerce.StockService.HttpApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class CategoryController() : ApiControllerBase
    {
        [HttpPost("resv")]
        public async Task<IActionResult> CreateStockResv(CreateStockResvCommand request)
        {
            var result = await Sender.Send(request);
            return ReturnResult(result);
        }
    }
}
