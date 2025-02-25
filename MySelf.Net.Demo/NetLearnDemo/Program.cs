
#region 创建webapplicationBuilder

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySelf.Net.Demo.MyInterface;
using MySelf.Net.Demo.MyService;
using MySelf.Net.Demo.NetLearnDemo.Utility;
using MySelf.AgileFramework.WebCore.ConfigurationExtend;
using MySelf.AgileFramework.WebCore.StartupExtend;
using MySelf.AgileFramework.WebCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
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
//builder.Services.AddSingleton<SecondMiddleware>();
//builder.Services.AddSingleton<SecondNewMiddleware>();


//builder.Services.Replace(ServiceDescriptor.Singleton<IMiddlewareFactory, SecondNewMiddlewareFactory>());
#endregion

#region 标准的middleware IOC注册
//builder.Services.AddBrowserFilter();//不允许edge

//builder.Services.AddBrowserFilter(options =>
//{
//    options.EnableEdge = true;
//});
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
#region HttpContext获取
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//如果需要获取HttpContext
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
//app.UseBrowserFilter();//请求会走BrowserFilterMiddleware
#endregion
//#region    框架默认中间件




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();//HTTP 严格传输安全
}
else
{
    app.UseDeveloperExceptionPage();//DeveloperExceptionPageMiddlewareImpl

    #region  扩展指定错误处理动作
    app.UseStatusCodePagesWithReExecute("/Error/{0}");//只要不是200 都能进来

    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            //还能判断是否Ajax请求，可以返回Json格式
            int errorCode = context.Response.StatusCode;

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html";

            await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            await context.Response.WriteAsync($"ERROR! {errorCode}<br><br>\r\n");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            Console.WriteLine($"{exceptionHandlerPathFeature?.Error.Message}");
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            await context.Response.WriteAsync($"{exceptionHandlerPathFeature?.Error.Message}<br><br>\r\n");

            // Use exceptionHandlerPathFeature to process the exception (for example, 
            // logging), but do NOT expose sensitive error information directly to 
            // the client.

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            {
                await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
            }

            await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
            await context.Response.WriteAsync("</body></html>\r\n");
            await context.Response.WriteAsync(new string(' ', 512)); // IE padding
        });
    });
    #endregion
}

//防盗链
app.UseRefuseStealing();

app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),//指定路径---还有provider
//    ServeUnknownFileTypes = false,//
//    OnPrepareResponse = context =>
//    {
//        context.Context.Response.Headers[HeaderNames.CacheControl] = "no-store";//"no-cache";//
//    }//响应请求之前，才可以修改header
//});


//app.UseDirectoryBrowser(new DirectoryBrowserOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
//    //FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory()),
//    //RequestPath = "/CustomImages"
//});//其实是个后门
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//#endregion
#region Run
app.Run();
#endregion
