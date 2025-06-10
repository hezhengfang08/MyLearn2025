using MySelf.MSACommerce.CartService.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.UseCases.Queries
{
 

    public record GetCartQuery(long UserId):IQuery<Result<CartDto>>;
    public class GetCartQueryHandler(ICartRepository cartRepostory, IMapper mapper)
    : IQueryHandler<GetCartQuery, Result<CartDto>>
    {
        public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await cartRepostory.GetCartAsync(request.UserId);
            if (cart is null) return Result.NotFound();
            var result = mapper.Map<CartDto>(cart);
            return Result.Success(result);
        }
    }

}
