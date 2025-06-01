using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient.Strategies
{
    public class RoundRobinStrategy:ILoadBalancingStrategy
    {
        private int _index;

        public string Resolve(List<string> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new InvalidOperationException("无可用节点");
            }

            _index = Interlocked.Increment(ref _index) % nodes.Count;
            return nodes[_index];
        }
    }
}
