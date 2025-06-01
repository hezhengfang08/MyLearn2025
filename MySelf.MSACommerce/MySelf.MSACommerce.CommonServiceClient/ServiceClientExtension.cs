using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.CommonServiceClient.AspNetCore;
using MySelf.MSACommerce.Consul.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient
{
    public static class ServiceClientExtension
    {
        public static IServiceCollection AddServiceClient<TServiceClient>(this IServiceCollection services,
            Action<ServiceClientOption> configServiceClient,
            Action<HttpClient> configHttpClient)
            where TServiceClient:class, ISeviceClient
        {
            var serviceClientOption = new ServiceClientOption();
            configServiceClient.Invoke(serviceClientOption);
            services.AddConsulDiscovery();
            services.AddLoadBalancer<TServiceClient>(serviceClientOption.LoadBalancingStrategy);
            services.AddHttpClient<TServiceClient>(configHttpClient);
            services.AddScoped<TServiceClient>();
            return services;
        }
    }
}
