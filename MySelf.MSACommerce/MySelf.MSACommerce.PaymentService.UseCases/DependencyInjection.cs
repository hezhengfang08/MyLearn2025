using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.PaymentService.UseCases.Apis;
using MySelf.MSACommerce.PaymentService.UseCases.CapSubscribes;
using MySelf.MSACommerce.UseCases.Common;
using MySelf.MSACommerce.CommonServiceClient;
using System.Reflection;

namespace MySelf.MSACommerce.PaymentService.UseCases
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
            services.AddServiceClient<IOrderServiceApi>(option =>
            {
                option.ServiceName = "MySelf.MSACommerce.OrderService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });

            services.AddServiceClient<ISeckillServiceApi>(option =>
            {
                option.ServiceName = "MySelf.MSACommerce.SeckillService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });

        }
    }
}
