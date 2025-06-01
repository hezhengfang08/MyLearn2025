using MySelf.MSACommerce.CommonServiceClient.Strategies;


namespace MySelf.MSACommerce.CommonServiceClient.AspNetCore
{
    public class LoadBalancer<T>(LoadBalancingStrategy strategy):LoadBalancer(strategy),ILoadBalancer<T> where T :class
    {
    }
}
