using Microsoft.AspNetCore.Mvc;

namespace MySelf.MSACommerce.ProductDetailPage.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
