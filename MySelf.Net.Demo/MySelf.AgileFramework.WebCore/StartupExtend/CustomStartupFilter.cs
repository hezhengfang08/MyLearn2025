using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.AgileFramework.WebCore.StartupExtend
{
    public class CustomStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> nextBuild)
        {
            return new Action<IApplicationBuilder>(
            app =>
            {
                //nextBuild.Invoke(app);//不next 后面就没法配置--放在前面，就后执行
                app.Use(next =>
                {
                    Console.WriteLine($"This is {nameof(CustomStartupFilter)} middleware 1");
                    return new RequestDelegate(
                        async context =>
                        {
                            context.Response.ContentType = "text/html";

                            await context.Response.WriteAsync($"This is {nameof(CustomStartupFilter)} Hello World xxxxx start");
                            await next.Invoke(context);
                            await context.Response.WriteAsync($"This is {nameof(CustomStartupFilter)} Hello World xxxxx   end");
                            await Task.Run(() => Console.WriteLine($"{nameof(CustomStartupFilter)} 12345678797989"));
                        });
                });
                nextBuild.Invoke(app);//不next 后面就没法配置--放在前面，就先执行
            }
           );
        }
    }
}
