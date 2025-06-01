using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient.Strategies
{
    public interface ILoadBalancingStrategy
    {
        string Resolve(List<string> nodes);
    }
}
