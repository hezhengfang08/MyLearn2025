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
        public static void AddServiceClient<TServiceApi>(this IServiceCollection services,
        Action<ServiceClientOption> configureServiceClient,
        Action<HttpClient> configureHttpClient)
        where TServiceApi : class
        {
            var serviceClientOption = new ServiceClientOption();
            configureServiceClient.Invoke(serviceClientOption);

            services.AddConsulDiscovery();

            services.AddLoadBalancer<TServiceApi>(serviceClientOption);

            services.AddHttpClient<TServiceApi>(configureHttpClient);

            services.AddScoped<IServiceClient<TServiceApi>, ServiceClient<TServiceApi>>();
        }
    }
}
