using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.Infrastructure.Common;
using MySelf.MSACommerce.Infrastructure.ElasticSearch;

namespace MySefl.MSACommerce.SearchService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);
            services.AddInfrastructureEs(configuration);
            return services;
        }
    }
}
