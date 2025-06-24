
using MySelf.MSACommerce.Authentication.JwtBearer;
using MySelf.MSACommerce.HttpApi.Common;
using MySelf.MSACommerce.PaymentService.Infrastructure;
using MySelf.MSACommerce.PaymentService.UseCases;
using System.Text.Json.Serialization;
using MySelf.MSACommerce.Configuration;

namespace MySelf.MSACommerce.PaymentService.HttpApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddConfigCenter("payment-service");

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddUseCase();

            builder.Services.AddHttpApi();

            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
    });

            builder.Services.AddJwtBearer(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpCommon();


            app.MapControllers();

            app.Run();
        }
    }
}
