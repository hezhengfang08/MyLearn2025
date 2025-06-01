using MySelf.MSACommerce.CommonServiceClient.Strategies;


namespace MySelf.MSACommerce.CommonServiceClient.AspNetCore
{
    public class LoadBalancer<T>(ServiceClientOption option) : LoadBalancer(option), ILoadBalancer<T> where T : class;
}
