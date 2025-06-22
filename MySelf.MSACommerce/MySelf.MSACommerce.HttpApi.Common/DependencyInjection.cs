using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
            services.AddHealthChecks();
            services.AddScoped<IUser, CurrentUser>();
            services.AddHttpContextAccessor();
            services.AddExceptionHandler<UseCaseExceptionHandler>();
            services.AddProblemDetails();
            ConfigureCors(services);
            ConfigureSwagger(services);
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


        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "电商平台 API 文档",
                    Version = "v1",
                    Description = "一个微服务架构的电商平台实战项目"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "JWT Bearer 认证",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] { }
                }
            });
            });
        }
    }
}
