using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

namespace MySelf.MSACommerce.WebGateway
{
    public class IPConsulServiceBuilder : DefaultConsulServiceBuilder
    {
        public IPConsulServiceBuilder(IHttpContextAccessor contextAccessor, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory) : base(contextAccessor, clientFactory, loggerFactory)
        {
        }
        protected override string GetDownstreamHost(ServiceEntry entry, Node node)
        {
            return entry.Service.Address;
            //return base.GetDownstreamHost(entry, node);
        }
    }
}
