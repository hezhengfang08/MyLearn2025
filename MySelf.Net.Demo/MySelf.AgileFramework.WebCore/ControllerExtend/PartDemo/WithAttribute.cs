using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using MySelf.AgileFramework.WebCore.ConventionExtend;

namespace MySelf.AgileFramework.WebCore.ControllerExtend.PartDemo
{
    /// <summary>
    /// 通过特性转成控制器
    /// </summary>
    [Controller]
    [CustomControllerModelConventionAttribute]
    public class WithAttribute
    {
        /// <summary>
        /// 没有继承Controller，viewdata不能用，所以注入个IModelMetadataProvider
        /// 要获取HttpContext---IHttpContextAccessor
        /// </summary>
        private IModelMetadataProvider _IModelMetadataProvider = null;
        /// <summary>
        /// 获取HttpContext，需要IOC注册
        /// </summary>
        private IHttpContextAccessor _IHttpContextAccessor;
        private HttpContext _HttpContext;
        public WithAttribute(IModelMetadataProvider modelMetadataProvider, IHttpContextAccessor httpContextAccessor)
        {
            this._IModelMetadataProvider = modelMetadataProvider;
            this._IHttpContextAccessor = httpContextAccessor;
            this._HttpContext = this._IHttpContextAccessor.HttpContext;
        }

        /// <summary>
        /// http://localhost:5726/WithAttribute/Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            ViewDataDictionary viewData = new ViewDataDictionary(this._IModelMetadataProvider, new ModelStateDictionary());
            viewData["Info"] = $"{nameof(WithAttribute)}.Info";

            return new ViewResult()
            {
                ViewName = "~/Views/Part/Index.cshtml",
                ViewData = viewData
            };

        }

        /// <summary>
        /// 
        /// 
        /// http://localhost:5726/WithAttribute/Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "WithAttribute-Get 222";
        }

        /// <summary>
        /// http://localhost:5726/WithAttribute/Convention
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Convention()
        {
            return $"WithAttribute-Convention  ConventionValue={this._HttpContext.GetRouteValue("ConventionValue")}";
        }
    }
}

