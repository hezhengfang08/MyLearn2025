
using MySelf.MSACommerce.Authentication.JwtBearer;
using MySelf.MSACommerce.StockService.Infrastructure;
using MySelf.MSACommerce.StockService.UseCases;

namespace MySelf.MSACommerce.StockService.HttpApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddUseCase();

            builder.Services.AddHttpApi();

            builder.Services.AddControllers();

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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
