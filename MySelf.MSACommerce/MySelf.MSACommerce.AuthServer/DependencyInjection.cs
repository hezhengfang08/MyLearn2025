using MySelf.MSACommerce.AuthServer.Apis;
using MySelf.MSACommerce.AuthServer.Services;
using Refit;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using MySelf.MSACommerce.CommonServiceClient;

namespace MySelf.MSACommerce.AuthServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthServer(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureSwagger(services);
            ConfigureRedis(services, configuration);
            ConfigureUserService(services, configuration);

            ConfigureIdentity(services, configuration);

            return services;
        }
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "鉴权中心",
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
                }
                );
        }

        private static void ConfigureRedis(IServiceCollection services, IConfiguration configuration)
        {
            var redisConnstr = configuration.GetConnectionString("RedisConnection");
            if (redisConnstr != null)
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnstr));
        }
        private static void ConfigureUserService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceClient<IUserServiceApi>(
                option =>
                {
                    option.ServiceName = "Zhaoxi.MSACommerce.UserService.HttpApi";
                    option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
                }, client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(1);
                });
    
        }

        private static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IIdentityService, IdentityService>();

            // 从配置文件中读取JwtSettings，并注入到容器中
            var configurationSection = configuration.GetSection(nameof(JwtSettings));
            var jwtSettings = configurationSection.Get<JwtSettings>();
            if (jwtSettings is null) throw new NullReferenceException(nameof(jwtSettings));
            services.Configure<JwtSettings>(configurationSection);
        }
    }
}
