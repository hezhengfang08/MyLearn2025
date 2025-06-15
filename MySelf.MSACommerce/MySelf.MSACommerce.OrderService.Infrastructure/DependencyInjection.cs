﻿using IdGen.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.Infrastructure.Common;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors;
using MySelf.MSACommerce.OrderService.Infrastructure.Data;

namespace MySelf.MSACommerce.OrderService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInfrastructureCommon(configuration);

            services.AddInfrastructureEfCore();

            ConfigureEfCore(services, configuration);

            ConfigureCap(services, configuration);

            services.AddIdGen(0);

            return services;
        }

        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("OrderDbConnection");

            services.AddDbContext<OrderDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>());
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }

        private static void ConfigureCap(IServiceCollection services, IConfiguration configuration)
        {
            var dbConn = configuration.GetConnectionString("OrderDbConnection");
            if (dbConn is null) throw new ArgumentNullException(nameof(dbConn));

            services.AddCap(x =>
            {
                x.UseEntityFramework<OrderDbContext>();
                x.UseMySql(dbConn);
                x.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
                x.UseDashboard();
            });
        }
    }
}
