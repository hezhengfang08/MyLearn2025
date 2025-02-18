using Microsoft.AspNetCore.Hosting;
using MySelf.AgileFramework.WebCore.StartupExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(CustomHostingStartupOut))]
namespace MySelf.AgileFramework.WebCore.StartupExtend
{
    public class CustomHostingStartupOut : IHostingStartup
    {
        public CustomHostingStartupOut()
        {
            Console.WriteLine($"********This is {nameof(CustomHostingStartupOut)} ctor********");
        }
        public void Configure(IWebHostBuilder builder)
        {
            Console.WriteLine($"********This is {nameof(CustomHostingStartupOut)} Configure********");

            //有IWebHostBuilder，一切都可以做。。
            #region 

            #endregion
        }
    }
}
