using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.SearchService.Core.Entities;
using MySelf.MSACommerce.SearchService.UseCases.Apis;
using MySelf.MSACommerce.SharedKernel.Paging;
using Result = MySelf.MSACommerce.SharedKernel.Result.Result;

namespace MySelf.MSACommerce.SearchService.UseCases.Queries
{
    public record GetProductQuery(string Keyword, Pagination Pagination) : IQuery<Result<SearchProductDto>>;
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(query => query.Keyword)
                .NotEmpty();
        }
    }
    public class GetProductQueryHandler(
     ElasticsearchClient client,
     IServiceClient<ICategoryServiceApi> categoryService,
     IServiceClient<IBrandServiceApi> brandService) : IQueryHandler<GetProductQuery, Result<SearchProductDto>>
    {
        public async Task<Result<SearchProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = new SearchProductDto();
            var docResponse = await client.SearchAsync<Product>(s => s
                .From((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Size(request.Pagination.PageSize)
                .Query(q => q.Match(m => m
                    .Field(f => f.All)
                    .Query(request.Keyword))), cancellationToken);

            var totalResponse = await client.SearchAsync<Product>(s => s
                 .Size(0)
                 .TrackTotalHits(new TrackHits(true))
                 .Query(q => q.Match(m => m
                     .Field(f => f.All)
                     .Query(request.Keyword))), cancellationToken);

            result.Products =
                new PagedList<Product>(docResponse.Documents.ToList(), totalResponse.Total, request.Pagination);
            result.Page = result.Products.MetaData;
            var categoryIds = result.Products.Select(p => p.CategoryId[2]).Distinct().ToArray();
            var brandIds = result.Products.Select(p => p.BrandId).Distinct().ToArray();

            var categoryResponse = await categoryService.ServiceApi.GetListAsync(categoryIds);
            if (categoryResponse.IsSuccessStatusCode) result.Categories = categoryResponse.Content;

            var brandResponse = await brandService.ServiceApi.GetBrandsAsync(brandIds);
            if (brandResponse.IsSuccessStatusCode) result.Brands = brandResponse.Content;

            return Result.Success(result);
        }
    }
}
