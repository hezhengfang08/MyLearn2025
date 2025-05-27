using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using MySelf.MSACommerce.HttpApi.Common.Services;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.HttpApi.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpApiCommon(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();
            services.AddHttpContextAccessor();
            services.AddExceptionHandler<UseCaseExceptionHandler>();
            services.AddProblemDetails();
            ConfigureCors(services);
            return services;
        }
        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(op =>
            {
                op.AddPolicy("AllowAny", bulder =>
                {
                    bulder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
