using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using MySelf.MSACommerce.ProductService.UseCases.Queries;

namespace MySelf.MSACommerce.ProductService.HttpApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController() : ApiControllerBase
    {
        [HttpGet("spu")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await Sender.Send(new GetSpuFullQuery(id));
            return ReturnResult(result);
        }
    }
}
