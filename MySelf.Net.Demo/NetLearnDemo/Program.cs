
#region 创建webapplicationBuilder

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySelf.Net.Demo.MyInterface;
using MySelf.Net.Demo.MyService;
using MySelf.Net.Demo.NetLearnDemo.Utility;
using MySelf.AgileFramework.WebCore.ConfigurationExtend;
using MySelf.AgileFramework.WebCore.StartupExtend;
using MySelf.AgileFramework.WebCore.MiddlewareExtend.SimpleExtend;
using MySelf.AgileFramework.WebCore.MiddlewareExtend;
var builder = WebApplication.CreateBuilder(args);

#endregion

#region  配置builder 
#region configuaration
builder.Configuration.AddJsonFile("customsettings.json", true, true);
builder.Configuration.AddXmlFile("appsettings.xml", true, true);
#region 内存Provider
var memoryConfig = new Dictionary<string, string>
                {
                   {"TodayMemory", "0114-Memory"},
                   {"RabbitMQOptions:HostName", "192.168.3.254-Memory"},
                    {"RabbitMQOptions:UserName", "guest-Memory"},
                     {"RabbitMQOptions:Password", "guest-Memory"}
                };
builder.Configuration.AddInMemoryCollection(memoryConfig);
#endregion
#endregion
#region 自定义模式

//((IConfigurationBuilder)builder.Configuration).Add(new CustomConfigurationSource());

//builder.Configuration.Add(new CustomConfigurationSource());
//A类 实现了B接口，B接口是有Add方法   
//但是用A类实例直接去调用Add方法报错
//但如果把A强制转换成B接口类型，就不报错了
//因为接口的Add方法是显式实现，那么调用时就必须转换成B接口类型---IConfigurationBuilder.Add

builder.Configuration.AddCustomConfiguration();
#endregion
#region ChangeToken
ChangeTokenTest.Show();
#endregion
#region 日志组件
builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddDebug();
builder.Logging.AddLog4Net();//Microsoft.Extensions.Logging.Log4Net.AspNetCore
builder.Logging.AddFilter("System", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
#endregion

#region IOC容器替换---
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory);
#endregion
#region Host--Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.MaxConcurrentUpgradedConnections = 100;
    options.Limits.MaxRequestBodySize = 1024 * 1024;//byte--有IIS代理后就失效了
    options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
    options.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
    //options.Listen(IPAddress.Loopback, 8000);
    //options.Listen(IPAddress.Loopback, 9000);

    //options.Listen(IPAddress.Loopback, 9099, o => o.Protocols =
    //     HttpProtocols.Http2);//命令行参数的端口就失效了
    //options.Listen(IPAddress.Loopback, 5001, listenOptions =>
    // {
    //     listenOptions.UseHttps("testCert.pfx", "testPassword");
    // });//没有本地证书
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
});

#endregion
#region SecondMiddleWare注册
builder.Services.AddSingleton<SecondMiddleware>();
builder.Services.AddSingleton<SecondNewMiddleware>();


//builder.Services.Replace(ServiceDescriptor.Singleton<IMiddlewareFactory, SecondNewMiddlewareFactory>());
#endregion

#region 标准的middleware IOC注册
//builder.Services.AddBrowserFilter();//不允许edge

builder.Services.AddBrowserFilter(options =>
{
    options.EnableEdge = true;
});
#endregion
#region IOC注册
#region 开发者注册
builder.Services.AddTransient<ITestServiceA, TestServiceA>();
builder.Services.AddTransient<ITestServiceA, TestServiceAV2>();//瞬时--注册的刷新，2个都在
builder.Services.AddSingleton<ITestServiceB, TestServiceB>();   //单例
builder.Services.AddScoped<ITestServiceC, TestServiceC>();//作用域单例--一次请求一个实例,
builder.Services.AddTransient<ITestServiceD, TestServiceD>();

builder.Services.AddTransient<ITestServiceE, TestServiceE>();
builder.Services.Replace(ServiceDescriptor.Transient<ITestServiceE, TestServiceEV2>());
#endregion

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
#endregion
#region IStartupFilter拓展
//builder.Services.AddTransient<IStartupFilter, CustomStartupFilter>();//
#endregion

#region Build
var app = builder.Build();
#endregion

#region UseMiddleware式--用类
//app.UseMiddleware<FirstMiddleware>();
//app.UseMiddleware<SecondMiddleware>();
//app.UseMiddleware<SecondNewMiddleware>();
//app.UseMiddleware<ThirdMiddleware>("Eleven Zhaoxi.NET7.DemoProject");
#endregion

#region 标准Middleware
app.UseBrowserFilter();//请求会走BrowserFilterMiddleware
#endregion
//#region    框架默认中间件




//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//#endregion
#region Run
app.Run();
#endregion
