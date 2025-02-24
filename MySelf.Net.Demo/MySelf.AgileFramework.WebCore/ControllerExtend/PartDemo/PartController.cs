using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MySelf.AgileFramework.WebCore.ControllerExtend.PartDemo
{
    /// <summary>
    /// dotnet run --urls="http://*:5726" ip="127.0.0.1" /port=5726 ConnectionStrings:Write=CommandLineArgument
    /// </summary>
    [Controller]//要么标记特性  要么Controller结尾
    public class PartController : Controller
    {
        #region Identity
        private readonly ILogger<PartController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        public PartController(ILogger<PartController> logger,
            ILoggerFactory loggerFactory)
        {
            this._logger = logger;
            this._loggerFactory = loggerFactory;
        }
        #endregion

        /// <summary>
        /// http://localhost:5726/part/index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            this._logger.LogWarning($"This is Zhaoxi.AgileFramework.WebCore.ControllerExtend.PartDemo.{nameof(PartController)}");

            base.ViewData["Info"] = $"{nameof(PartController)}.Info";

            return View();
        }
    }
}
