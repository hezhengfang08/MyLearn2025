
#region ����webapplicationBuilder

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

#region  ����builder 
#region configuaration
builder.Configuration.AddJsonFile("customsettings.json", true, true);
builder.Configuration.AddXmlFile("appsettings.xml", true, true);
#region �ڴ�Provider
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
#region �Զ���ģʽ

//((IConfigurationBuilder)builder.Configuration).Add(new CustomConfigurationSource());

//builder.Configuration.Add(new CustomConfigurationSource());
//A�� ʵ����B�ӿڣ�B�ӿ�����Add����   
//������A��ʵ��ֱ��ȥ����Add��������
//�������Aǿ��ת����B�ӿ����ͣ��Ͳ�������
//��Ϊ�ӿڵ�Add��������ʽʵ�֣���ô����ʱ�ͱ���ת����B�ӿ�����---IConfigurationBuilder.Add

builder.Configuration.AddCustomConfiguration();
#endregion
#region ChangeToken
ChangeTokenTest.Show();
#endregion
#region ��־���
builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddDebug();
builder.Logging.AddLog4Net();//Microsoft.Extensions.Logging.Log4Net.AspNetCore
builder.Logging.AddFilter("System", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
#endregion

#region IOC�����滻---
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory);
#endregion
#region Host--Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 100;
    options.Limits.MaxConcurrentUpgradedConnections = 100;
    options.Limits.MaxRequestBodySize = 1024 * 1024;//byte--��IIS������ʧЧ��
    options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
    options.Limits.MinResponseDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
    //options.Listen(IPAddress.Loopback, 8000);
    //options.Listen(IPAddress.Loopback, 9000);

    //options.Listen(IPAddress.Loopback, 9099, o => o.Protocols =
    //     HttpProtocols.Http2);//�����в����Ķ˿ھ�ʧЧ��
    //options.Listen(IPAddress.Loopback, 5001, listenOptions =>
    // {
    //     listenOptions.UseHttps("testCert.pfx", "testPassword");
    // });//û�б���֤��
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
});

#endregion
#region SecondMiddleWareע��
builder.Services.AddSingleton<SecondMiddleware>();
builder.Services.AddSingleton<SecondNewMiddleware>();


//builder.Services.Replace(ServiceDescriptor.Singleton<IMiddlewareFactory, SecondNewMiddlewareFactory>());
#endregion

#region ��׼��middleware IOCע��
//builder.Services.AddBrowserFilter();//������edge

builder.Services.AddBrowserFilter(options =>
{
    options.EnableEdge = true;
});
#endregion
#region IOCע��
#region ������ע��
builder.Services.AddTransient<ITestServiceA, TestServiceA>();
builder.Services.AddTransient<ITestServiceA, TestServiceAV2>();//˲ʱ--ע���ˢ�£�2������
builder.Services.AddSingleton<ITestServiceB, TestServiceB>();   //����
builder.Services.AddScoped<ITestServiceC, TestServiceC>();//��������--һ������һ��ʵ��,
builder.Services.AddTransient<ITestServiceD, TestServiceD>();

builder.Services.AddTransient<ITestServiceE, TestServiceE>();
builder.Services.Replace(ServiceDescriptor.Transient<ITestServiceE, TestServiceEV2>());
#endregion

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();
#endregion
#region IStartupFilter��չ
//builder.Services.AddTransient<IStartupFilter, CustomStartupFilter>();//
#endregion

#region Build
var app = builder.Build();
#endregion

#region UseMiddlewareʽ--����
//app.UseMiddleware<FirstMiddleware>();
//app.UseMiddleware<SecondMiddleware>();
//app.UseMiddleware<SecondNewMiddleware>();
//app.UseMiddleware<ThirdMiddleware>("Eleven Zhaoxi.NET7.DemoProject");
#endregion

#region ��׼Middleware
app.UseBrowserFilter();//�������BrowserFilterMiddleware
#endregion
//#region    ���Ĭ���м��




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
