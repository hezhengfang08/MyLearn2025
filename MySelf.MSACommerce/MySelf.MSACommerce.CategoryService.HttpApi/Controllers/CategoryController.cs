using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.CategoryService.UseCases.Queries;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;

namespace MySelf.MSACommerce.CategoryService.HttpApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController() : ApiControllerBase
    {
        [HttpGet("parents")]
        public async Task<IActionResult> GetParents(long id)
        {
            var result = await Sender.Send(new GetCategoryAndParentsQuery(id));
            return ReturnResult(result);
        }

        [HttpGet("children")]
        public async Task<IActionResult> GetChildren(long id)
        {
            var result = await Sender.Send(new GetCategoryAndChildrenQuery(id));
            return ReturnResult(result);
        }
    }

}
