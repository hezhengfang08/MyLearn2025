using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.Consul.ServiceDiscovery
{
    public interface IServiceDiscovery
    {
    Task<List<string>> GetServicesAsync(string serviceName);
    }
}
