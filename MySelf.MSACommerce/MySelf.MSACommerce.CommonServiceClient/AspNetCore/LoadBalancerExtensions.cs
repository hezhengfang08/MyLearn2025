using Microsoft.Extensions.DependencyInjection;


namespace MySelf.MSACommerce.CommonServiceClient.AspNetCore
{
    public static class LoadBalancerExtensions
    {
        public static IServiceCollection AddLoadBalancer<T>(this IServiceCollection services, LoadBalancingStrategy strategy) where T : class
        {
            services.AddSingleton<ILoadBalancer<T>>(new LoadBalancer<T>(strategy));
            return services;
        }
    }
}
