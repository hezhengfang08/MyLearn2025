using MySelf.MSACommerce.SeckillService.Infrastructure;

namespace MySelf.MSACommerce.SecKillSyncWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.ConfigureQuartz(builder.Configuration);


            var host = builder.Build();
            host.Run();
        }
    }
}