using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.HttpApi.Common;
using MySelf.MSACommerce.UserService.HttpApi.Apis;

namespace MySelf.MSACommerce.UserService.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpApi(this IServiceCollection services)
        {
            services.AddHttpApiCommon();
            ConfigureVerificationServer(services);
            return services;
        }
        private static void ConfigureVerificationServer(IServiceCollection services)
        {
            services.AddServiceClient<IVerificationApi>(option =>
            {
                option.ServiceName = "Zhaoxi.MSACommerce.VerificationServer";
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(5);
            });
        }
    }
}
