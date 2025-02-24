using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.AgileFramework.WebCore.ControllerExtend.PartDemo
{
    /// <summary>
    /// 
    /// http://localhost:5726/api/NoInherit
    /// </summary>
    [Route("api/[controller]")]
    public class NoInheritController
    {
        [HttpGet]
        public string Get()
        {
            return "NoInheritController 111";
        }
    }
}
