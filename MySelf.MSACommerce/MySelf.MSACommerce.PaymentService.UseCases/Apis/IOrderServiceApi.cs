using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Apis
{
    public interface IOrderServiceApi
    {
        [Get("/api/order")]
        Task<IApiResponse<OrderDto>> GetOrderAsync(long id);
    }
}
