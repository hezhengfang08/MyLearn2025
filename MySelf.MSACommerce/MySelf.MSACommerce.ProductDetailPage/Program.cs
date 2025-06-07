using MySelf.MSACommerce.ProductDetailPage.Services;
using MySelf.MSACommerce.ProductDetailPage;
using MySelf.MSACommerce.Consul.ServiceRegistration;
using Consul.AspNetCore;
using MySelf.MSACommerce.Consul.ServiceDiscovery;
namespace MySelf.MSACommerce.ProductDetailPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDetailPageService, DetailPageService>();
            builder.Services.ConfigureService(builder.Configuration);
            var serviceCheck = builder.Configuration.GetSection("ServiceCheck").Get<ServiceCheckConfiguration>();
            serviceCheck ??= new ServiceCheckConfiguration();
       
            builder.Services.AddConsulService(serviceConfiguration =>
            {
                serviceConfiguration.ServiceAddress = new Uri(builder.Configuration["urls"] ?? builder.Configuration["applicationUrl"]);
            }, serviceCheck);

            builder.Services.AddConsul();
            builder.Services.AddConsulDiscovery();
            builder.Services.AddHealthChecks();

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            /// Configure the HTTP request pipeline.

            app.UseStaticPageMiddleware(@"d:\staticfiles\");

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllers();
            app.Run();
        }
    }
}
