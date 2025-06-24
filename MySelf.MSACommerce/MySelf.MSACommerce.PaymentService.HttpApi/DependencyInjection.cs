using MySelf.MSACommerce.HttpApi.Common;

namespace MySelf.MSACommerce.PaymentService.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpApi(this IServiceCollection services)
        {
            services.AddHttpApiCommon();

            return services;
        }
    }

}
