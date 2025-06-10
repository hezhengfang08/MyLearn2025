using MySelf.MSACommerce.CartService.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.UseCases.Commands
{
    public record DeleteItemCommand(long UserId, long SkuId) : ICommand<Result>;

    public class DeleteItemCommandHandler(
        ICartRepository cartRepository) : ICommandHandler<DeleteItemCommand, Result>
    {
        public async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            await cartRepository.RemoveItemAsync(request.UserId, request.SkuId);
            return Result.Success();
        }
    }
}
