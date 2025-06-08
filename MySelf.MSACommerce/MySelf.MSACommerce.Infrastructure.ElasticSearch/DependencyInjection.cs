using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MySelf.MSACommerce.Infrastructure.ElasticSearch
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEs(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureElasticSearch(services, configuration);
            return services;
        }
        private static void ConfigureElasticSearch(IServiceCollection services, IConfiguration configuration)
        {
            var esConnnection = configuration.GetConnectionString("ElasticSearchConnection");
            if (string.IsNullOrEmpty(esConnnection)) throw new ArgumentNullException("ElasticSearchConnection");
            var settings = new ElasticsearchClientSettings(new Uri(esConnnection));
            var client =new ElasticsearchClient(settings);
            services.AddSingleton(client);
        }
    }
}
