using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.Consul.ServiceDiscovery
{
    public class ConsulServiceDiscovery(IConsulClient consulClient) : IServiceDiscovery
    {
        public async Task<List<string>> GetServicesAsync(string serviceName)
        {
            var queryResult = await consulClient.Health.Service(serviceName, null, true);
            return queryResult.Response.Select(serviceEntry => serviceEntry.Service.Address + ":"
            + serviceEntry.Service.Port).ToList();
        }
    }
}
