using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient
{
    public interface IServiceClient<TServiceApi> where TServiceApi : class
    {
    
        string ServiceName { get; set; }
        TServiceApi ServiceApi { get; set; }
    }
}
