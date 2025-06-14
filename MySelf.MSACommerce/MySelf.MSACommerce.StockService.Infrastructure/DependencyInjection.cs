using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.Infrastructure.Common;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore;
using MySelf.MSACommerce.StockService.Infrastructure.Data;

namespace MySelf.MSACommerce.StockService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);

            services.AddInfrastructureEfCore();

            ConfigureEfCore(services, configuration);

            return services;
        }

        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("StockDbConnection");

            services.AddDbContext<StockDbContext>((sp, options) =>
            {
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }
    }
}
