using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases.Apis
{
    public interface IStockServiceApi
    {
        [Post("/api/stock/resv")]
        Task<IApiResponse> CreateStockResvAsync(long skuId, long orderId, int quantity);
    }
}
