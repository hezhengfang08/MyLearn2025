using Consul.AspNetCore;
using MassTransit;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.StaticPageWorker.Apis;
using MySelf.MSACommerce.StaticPageWorker.Consumers;

namespace MySelf.MSACommerce.StaticPageWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddConsul();

            builder.Services.AddServiceClient<IProductDetailPage>(option =>
            {
                option.ServiceName = "Zhaoxi.MSACommerce.ProductDetailPage";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });

            builder.Services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<ProductUpdatedConsumer>();
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("RabbitMqConnection"));
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(10)));
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
