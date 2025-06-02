using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.Infrastructure.EntityFrameworkCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEfCore(this IServiceCollection services)
        {
            services.AddScoped<AuditEntityInterceptor>();

            return services;
        }
    }
}
