using Microsoft.AspNetCore.Mvc;

namespace MySelf.Net.Demo.NetLearnDemo.Controllers
{
    public class MiddlewareController : Controller
    {
        //[FromQuery]//不是IOC注入的，而是靠参数绑定
        //public string Name { get; set; }

        private readonly ILogger<MiddlewareController> _logger;
        private readonly IConfiguration _configuration =null;

        public MiddlewareController(ILogger<MiddlewareController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// dotnet run --urls="http://*:5726" ip="127.0.0.1" /port=5726 ConnectionStrings:Write=CommandLineArgument
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///         /// http://localhost:5726/Middleware/Exceptions/1
        /// http://localhost:5726/Middleware/Exceptions/2
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Exceptions(int? Id)
        {
            if(Id == 1)
            {
                throw new Exception($"MiddlewareController--Exceptions---{nameof(Id)}={Id}");
            }
            return View();
        }
    }
}
