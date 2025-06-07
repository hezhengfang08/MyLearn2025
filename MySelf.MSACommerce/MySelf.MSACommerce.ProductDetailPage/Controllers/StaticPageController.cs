using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.ProductDetailPage.Services;

namespace MySelf.MSACommerce.ProductDetailPage.Controllers
{
   
    [ApiController]
    public class StaticPageController(IStaticPageService staticPageService) : ControllerBase
    {
        [HttpDelete("/item/{id:long}.html")]
        public IActionResult Delete(long id)
        {
            var result = staticPageService.DeletePage(id);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }
    }
}
