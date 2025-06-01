using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient
{
    public interface ILoadBalancer
    {
        string GetNode(List<string> nodes); 
    }
}
