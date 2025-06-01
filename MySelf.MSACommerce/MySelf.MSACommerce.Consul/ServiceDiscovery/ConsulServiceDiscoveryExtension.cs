using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.Consul.ServiceDiscovery
{
    public static class ConsulServiceDiscoveryExtension
    {
        public static IServiceCollection AddConsulDiscovery(this IServiceCollection services) 
        {
            services.TryAddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
            return services;
        }
    }
}
