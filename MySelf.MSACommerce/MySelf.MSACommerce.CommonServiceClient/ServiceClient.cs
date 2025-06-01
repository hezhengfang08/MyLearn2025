using MySelf.MSACommerce.CommonServiceClient.AspNetCore;
using MySelf.MSACommerce.Consul.ServiceDiscovery;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CommonServiceClient
{
    public abstract class ServiceClient<TServiceApi> : IServiceClient<TServiceApi> where TServiceApi : class
    {
        public  string ServiceName { get; set; }
        public TServiceApi ServiceApi { get; set; }
        protected ServiceClient(IServiceDiscovery serviceDiscovery,
            ILoadBalancer<TServiceApi> loadBalancer,
            HttpClient httpClient) {
            ServiceName = loadBalancer.ServiceName;
            var serviceList = serviceDiscovery.GetServicesAsync(ServiceName).Result;
            var serviceAddress = loadBalancer.GetNode(serviceList);

            httpClient.BaseAddress = new Uri($"http://{serviceAddress}");
            ServiceApi = RestService.For<TServiceApi>(httpClient);

        }
    }
}
