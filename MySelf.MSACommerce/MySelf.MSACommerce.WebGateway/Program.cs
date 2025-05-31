
using MySelf.MSACommerce.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace MySelf.MSACommerce.WebGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.AddOcelot(
                folder: "./ocelot",
                env: builder.Environment,
                mergeTo: MergeOcelotJson.ToMemory,
                optional: false,
                reloadOnChange: true
                );

               
            builder.Services.AddOcelot()
                .AddConsul<IPConsulServiceBuilder>();

            builder.Services.AddJwtBearer(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway V1");
                    options.SwaggerEndpoint("/auth/swagger.json", "AuthServer V1");
                    options.SwaggerEndpoint("/user/swagger.json", "UserService V1");
                });
            }

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
