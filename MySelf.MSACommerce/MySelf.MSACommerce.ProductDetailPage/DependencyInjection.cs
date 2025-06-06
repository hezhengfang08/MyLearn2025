using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.ProductDetailPage.Apis;

namespace MySelf.MSACommerce.ProductDetailPage
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureServiceClient(services, configuration);

            ConfigureCors(services);

            return services;
        }
        private static void ConfigureServiceClient(IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceClient<IProductServiceApi>(options =>
            {
                options.ServiceName = @"MySelf.MSACommerce.ProductService.HttpApi";
                options.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });
            services.AddServiceClient<ICategoryServiceApi>(options =>
            {
                options.ServiceName = @"MySelf.MSACommerce.CategoryService.HttpApi";
                options.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });
            services.AddServiceClient<IBrandServiceApi>(options =>
            {
                options.ServiceName = @"MySelf.MSACommerce.IBrandServiceApi.HttpApi";
                options.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });
        }
        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}
