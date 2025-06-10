using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MySelf.MSACommerce.Infrastructure.Redis
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureRedis(services, configuration);

            return services;
        }
        private static void ConfigureRedis(IServiceCollection services, IConfiguration configuration)
        {
            var redisConnection = configuration.GetConnectionString("RedisConnection");
            if (string.IsNullOrEmpty(redisConnection))
            {
                throw new ArgumentNullException(nameof(redisConnection));
            }
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
            services.AddScoped<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        }
    }
}
