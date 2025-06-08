using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.SearchService.UseCases.Apis;
using MySelf.MSACommerce.UseCases.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SearchService.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddUseCaseCommon(Assembly.GetExecutingAssembly());
            ConfigureServiceClient(services);
            return services;
        }

        private static void ConfigureServiceClient(IServiceCollection services)
        {
            services.AddServiceClient<ICategoryServiceApi>(option =>
            {
                option.ServiceName = "Zhaoxi.MSACommerce.CategoryService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });

            services.AddServiceClient<IBrandServiceApi>(option =>
            {
                option.ServiceName = "Zhaoxi.MSACommerce.BrandService.HttpApi";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(2);
            });
        }

    }
}
