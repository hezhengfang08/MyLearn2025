using IdGen.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.Infrastructure.Common;
using MySelf.MSACommerce.Infrastructure.Redis;
using MySelf.MSACommerce.SeckillService.Infrastructure.Data;

namespace MySelf.MSACommerce.SeckillService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);

            services.AddInfrastructureRedis(configuration);

            ConfigureEfCore(services, configuration);

            services.AddIdGen(0);

            return services;
        }

        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("SecKillDbConnection");

            services.AddDbContext<SecKillDbContext>((sp, options) =>
            {
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }
    }
}
