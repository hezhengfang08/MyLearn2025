using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.OrderService.UseCases.Apis;
using MySelf.MSACommerce.OrderService.UseCases.CapSubscribes;
using MySelf.MSACommerce.UseCases.Common;
using System.Reflection;


namespace MySelf.MSACommerce.OrderService.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddUseCaseCommon(Assembly.GetExecutingAssembly());
            services.AddTransient<IOrderSubscriber, OrderSubscriber>();

            ConfigureServiceClient(services);

            return services;
        }

        private static void ConfigureServiceClient(IServiceCollection services)
        {
            services.AddServiceClient<IStockServiceApi>(option =>
            {
                option.ServiceName = "Zhaoxi.MSACommerce.StockService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });

            services.AddServiceClient<IProductServiceApi>(option =>
            {
                option.ServiceName = "Zhaoxi.MSACommerce.ProductService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });
        }
    }

}
