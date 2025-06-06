using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.ProductDetailPage.Apis;
using MySelf.MSACommerce.SharedKernel.Result;
using Refit;

namespace MySelf.MSACommerce.ProductDetailPage.Services
{
    public class DetailPageService(IServiceClient<IProductServiceApi> productClent,
        IServiceClient<ICategoryServiceApi> categoryClient,
        IServiceClient<IBrandServiceApi> brandClient) : IDetailPageService
    {
        public async Task<Result<Dictionary<string, object>>> GetSpuModel(long id)
        {
            var spuResponse = await productClent.ServiceApi.GetSpuAsync(id);
            if(!spuResponse.IsSuccessStatusCode || spuResponse.Content is null)
                return Result.NotFound("商品不存在");
            var spu = spuResponse.Content;
            if (spu.Status == 0)
                return Result.NotFound("商品未上架");
            var brankTask = brandClient.ServiceApi.GetBrandAsync(spu.BrandId);
            var categoryTask = categoryClient.ServiceApi.GetParents(spu.CategoryId);
            var specTask = categoryClient.ServiceApi.GetSpecs(spu.CategoryId);
            var parameterTask = categoryClient.ServiceApi.GetParameters(spu.CategoryId);
            await Task.WhenAll(brankTask, categoryTask, specTask, parameterTask);

            var brankResponse = await brankTask;
            var categoryResponse = await categoryTask;
            var specResponse = await specTask;
            var parameterResponse = await parameterTask;
            var model = new Dictionary<string, object>
            {
                {"spu", spu },
                {"skus",spu.Skus },
                {"detail",spu.Detail },
                {"brand",brankResponse.Content },
                {"categories", categoryResponse.Content},
                {"specs",specResponse.Content },
                {"parameterGroups", parameterResponse.Content}
            };

            return Result.Success(model);
        }
        
    }
}
