using MassTransit;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.SharedEvent.Products;
using MySelf.MSACommerce.StaticPageWorker.Apis;
using static MassTransit.ValidationResultExtensions;

namespace MySelf.MSACommerce.StaticPageWorker.Consumers
{
    public class ProductUpdatedConsumer(IServiceClient<IProductDetailPage> client) : IConsumer<ProductUpdateEvent>
    {
        public async Task Consume(ConsumeContext<ProductUpdateEvent> context)
        {
            Console.WriteLine("ProductUpdatedEvent: {0}", context.Message.SpuId);
            var result = await client.ServiceApi.DeletePageAsync(context.Message.SpuId);
            Console.WriteLine("DeletePage: {0}", result.IsSuccessStatusCode);
        }
    }
}
