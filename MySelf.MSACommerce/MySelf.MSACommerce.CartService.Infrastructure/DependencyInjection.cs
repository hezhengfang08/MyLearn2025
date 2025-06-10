using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.CartService.Core.Data;
using MySelf.MSACommerce.CartService.Infrastructure.Data;
using MySelf.MSACommerce.Infrastructure.Common;
using MySelf.MSACommerce.Infrastructure.Redis;

namespace MySelf.MSACommerce.CartService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);
            services.AddInfrastructureRedis(configuration);
            services.AddScoped<ICartRepository, RedisCartRepository>();
            return services;
        }
    }
}
