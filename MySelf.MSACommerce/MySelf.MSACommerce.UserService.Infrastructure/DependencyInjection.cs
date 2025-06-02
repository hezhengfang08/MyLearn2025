using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors;
using MySelf.MSACommerce.UserService.Infrastructure.Datas;
using System.Data.Common;
using System.Runtime.CompilerServices;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore;
using MySelf.MSACommerce.Infrastructure.Common;

namespace MySelf.MSACommerce.UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);
            ConfigureEfCore(services,configuration);
            return services;
        }
        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("UserDbConnection");
            services.AddDbContext<UserDbContext>((sp, options) =>
            {
                // add common audit field value
                options.AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>());
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }
    }
}
