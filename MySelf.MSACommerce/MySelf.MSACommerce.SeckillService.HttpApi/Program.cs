
using MySelf.MSACommerce.Authentication.JwtBearer;
using MySelf.MSACommerce.SeckillService.Infrastructure;
using MySelf.MSACommerce.SeckillService.UseCases;

namespace MySelf.MSACommerce.SeckillService.HttpApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddUseCase();

            builder.Services.AddHttpApi();

            builder.Services.AddControllers();

            builder.Services.AddJwtBearer(builder.Configuration);

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
