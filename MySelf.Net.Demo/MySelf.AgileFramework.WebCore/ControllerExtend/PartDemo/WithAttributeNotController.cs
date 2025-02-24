using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.AgileFramework.WebCore.ControllerExtend.PartDemo
{
    /// <summary>
    /// http://localhost:5726/api/WithAttributeNot
    /// </summary>
    //[NonController]
    [Route("api/[controller]")]
    public class WithAttributeNotController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "WithAttributeNotController-Get 333";
        }
    }
}

