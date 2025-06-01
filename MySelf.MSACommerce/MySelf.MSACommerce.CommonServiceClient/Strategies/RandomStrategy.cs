using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient.Strategies
{
    public class RandomStrategy:ILoadBalancingStrategy
    {
        private readonly Random _random =new ();

        public string Resolve(List<string> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new InvalidOperationException("无可用节点");
            }
            var index = _random.Next(nodes.Count);
            return nodes[index];    
        }
    }
}
