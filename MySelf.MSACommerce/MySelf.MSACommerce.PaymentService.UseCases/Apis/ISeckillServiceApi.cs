using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.UseCases.Apis
{
    public interface ISeckillServiceApi
    {
        [Get("/api/seckill/order/{userId}")]
        Task<IApiResponse<SecKillOrderDto>> GetOrderAsync(long userId);
    }
}
