using Elastic.Clients.Elasticsearch;
using MassTransit;
using MySelf.MSACommerce.CommonServiceClient;
using MySelf.MSACommerce.ElasticSyncWorker.Apis;
using MySelf.MSACommerce.ElasticSyncWorker.Models;
using MySelf.MSACommerce.SharedKernel.Paging;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ElasticSyncWorker.Consumers
{
    public class ProductFullSyncConsumer(
        IServiceClient<IProductServiceApi> productClient,
        IServiceClient<ICategoryServiceApi> categoryClient,
        IServiceClient<IBrandServiceApi> brandClient,
        ElasticsearchClient elasticsearchClient) : IConsumer<ProductFullSyncConsumer>
    {
        private readonly Dictionary<long, List<SpecKeyDto>> _specKeyDic = new();
        private readonly Dictionary<long, List<CategoryDto>> _categoryKeyDic = new();
        private readonly Dictionary<long, BrandDto> _brandKeyDic = new();

        public async Task Consume(ConsumeContext<ProductFullSyncConsumer> context)
        {
            var pageNumber = 1;
            const int pageSize = 100;
            bool hasNext;
            var indexName = "product";
            await elasticsearchClient.Indices.DeleteAsync(indexName);
            await elasticsearchClient.Indices.CreateAsync(indexName);
            do
            {
                var products = new List<Product>();
                var spuListResponse = await productClient.ServiceApi.GetSpuListAsync(pageNumber, pageSize);
                var paginationHeader = spuListResponse.Headers.First(pair => pair.Key == "Pagination").Value.First();
                if (spuListResponse.Content is null) return;

                if (paginationHeader == null)
                {
                    throw new Exception("Pagination header is null");
                }
                var pagination = JsonSerializer.Deserialize<PagedMetaData>(paginationHeader)!;
                hasNext = pagination.HasNext;
                var spuList = spuListResponse.Content.Where(spu => spu.Status == 1);
                foreach (var spu in spuList)
                {
                    try
                    {
                        var product = await BuildProductAsync(spu);
                        products.Add(product);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(spu.Id);
                        Console.WriteLine(e.Message);
                    }
                }

                pageNumber++;


                var response = await elasticsearchClient.IndexManyAsync(products, indexName);
                Console.WriteLine(response.DebugInformation);
            } while (hasNext);
        }
    

    private async Task<Product> BuildProductAsync(SpuDto spu)
        {
            var brand = await GetBrandAsync(spu.BrandId);
            var categories = await GetCategoriesAsync(spu.CategoryId);
            var specs = await GetSpecsAsync(spu.CategoryId);

            var product = new Product();
            product.Id = spu.Id;
            product.BrandId = spu.BrandId;

            if (spu.Skus == null || !spu.Skus.Any())
            {
                throw new Exception("商品 SKU 不存在");
            }

            product.Skus = JsonSerializer.Serialize(spu.Skus);

            // 查询商品分类名称组成的集合
            var cnames = categories.Select(c => c.Name).ToList();
            product.CategoryId = categories.Select(c => c.Id).ToArray();

            // 正则表达式用于从字符串中移除HTML标签
            var regex = new Regex(@"<[^>]+>|</[^>]+>");
            product.Description = regex.Replace(spu.Description, "");

            // 所有的搜索字段拼接到all中，all存入索引库，并进行分词处理，搜索时与all中的字段进行匹配查询
            product.All = product.Description + " " + string.Join(" ", cnames) + " " + brand.Name;

            // 存储skus的json结构的集合(JSON转换为字典)
            var skus = new List<Dictionary<string, object>>();
            foreach (var sku in spu.Skus)
            {
                // 存储价格
                product.Price.Add(sku.Price);
                var dict = new Dictionary<string, object>();
                dict.Add("id", sku.Id);
                dict.Add("name", sku.Name);
                //sku中有多个图片，只展示第一张
                dict.Add("image", sku.Images.Split(",")[0]);
                dict.Add("price", sku.Price);
                // 添加到字典中
                skus.Add(dict);
            }

            // SPU 规格值
            Dictionary<long, List<string>> spuSpec = JsonSerializer.Deserialize<Dictionary<long, List<string>>>(spu.Detail.Spec);

            //组装SPU规格键值对，key是品类规格名称，值是SPU规格值
            foreach (var specKey in specs)
            {
                //key是规格参数的名称
                var key = specKey.Name;
                var value = spuSpec[specKey.Id];
                //存入map
                product.Specs.Add(key, value);
            }

            return product;
        }

        private async Task<List<CategoryDto>> GetCategoriesAsync(long id)
        {
            if (_categoryKeyDic.TryGetValue(id, out var categoryList))
            {
                return categoryList;
            }
            var response = await categoryClient.ServiceApi.GetParents(id);
            var categories = response.Content;
            _categoryKeyDic.Add(id, categories);
            return categories;
        }

        private async Task<List<SpecKeyDto>> GetSpecsAsync(long id)
        {
            if (_specKeyDic.TryGetValue(id, out var specList))
            {
                return specList;
            }
            var response = await categoryClient.ServiceApi.GetSpecs(id);
            var specs = response.Content;
            _specKeyDic.Add(id, specs);
            return specs;
        }

        private async Task<BrandDto> GetBrandAsync(long id)
        {
            if (_brandKeyDic.TryGetValue(id, out var brandDto))
            {
                return brandDto;
            }

            var response = await brandClient.ServiceApi.GetBrandAsync(id);
            var brand = response.Content;
            _brandKeyDic.Add(id, brand);
            return brand;
        }
    }
}
