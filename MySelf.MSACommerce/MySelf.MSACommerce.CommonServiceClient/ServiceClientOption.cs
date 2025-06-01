using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient
{
    public class ServiceClientOption
    {
        public LoadBalancingStrategy LoadBalancingStrategy { get; set; } = LoadBalancingStrategy.RoundRobin;
        public string ServiceName { get; set; } = null!;

    }
}
