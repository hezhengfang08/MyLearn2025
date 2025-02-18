using MySelf.Net.Demo.MyInterface;
using MySelf.Net.Demo.MyService;
using MySelf.Net.Demo.NetLearnDemo.Utility;

[assembly:HostingStartup(typeof(CustomHostingStartupIn))]
namespace MySelf.Net.Demo.NetLearnDemo.Utility
{
    public class CustomHostingStartupIn : IHostingStartup
    {
        public CustomHostingStartupIn()
        {
            Console.WriteLine($"********This is {nameof(CustomHostingStartupIn)} ctor********");
        }
        public void Configure(IWebHostBuilder builder)
        {
            Console.WriteLine($"********This is {nameof(CustomHostingStartupIn)} Configure********");

            //有IWebHostBuilder，一切都可以做。。
            #region 
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddXmlFile("appsettings.xml", optional: false, reloadOnChange: true);
            });//添加配置

            builder.ConfigureServices(services =>
            {
                services.AddTransient<ITestServiceA, TestServiceA>();
            });//IOC注册

            builder.Configure(app =>
            {
                app.Use(next =>
                {
                    Console.WriteLine("This is CustomHostingStartup-Middleware  Init");
                    return new RequestDelegate(
                        async context =>
                        {
                            Console.WriteLine("This is CustomHostingStartup-Middleware start");
                            //await next.Invoke(context);//先不执行后面的
                            await Task.CompletedTask;
                            Console.WriteLine("This is CustomHostingStartup-Middleware end");
                        });
                });
            });//甚至来个中间件
            #endregion
        }
    }
}
