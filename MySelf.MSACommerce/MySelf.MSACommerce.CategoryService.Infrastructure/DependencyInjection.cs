using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.CategoryService.Infrastructure.Data;
using MySelf.MSACommerce.Infrastructure.Common;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;

namespace MySelf.MSACommerce.CategoryService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);
            services.AddInfrastructureEfCore();

            ConfigureEfCore(services, configuration);

            ConfigureCache(services, configuration);

            return services;
        }
        private static void ConfigureEfCore(IServiceCollection services,IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("CategoryDbConnection");
            services.AddDbContext<CategoryDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>());
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }
        private static void ConfigureCache(IServiceCollection services, IConfiguration configuration)
        {
            var redisConnection = configuration.GetConnectionString("RedisConnection");
            if (redisConnection != null) {
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
            }
            services.AddStackExchangeRedisCache(options => options.Configuration = redisConnection);
            services.AddFusionCache()
                .WithSystemTextJsonSerializer()
                .WithDistributedCache(provider => provider.GetRequiredService<IDistributedCache>());

        }
    }
}
