using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.StaticPageWorker.Apis
{
    public interface IProductDetailPage
    {
        [Delete("item/{id}.html")]
        Task<IApiResponse> DeletePageAsync(long id);
    }
}
