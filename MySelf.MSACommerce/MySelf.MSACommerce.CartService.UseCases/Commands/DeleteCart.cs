using MySelf.MSACommerce.CartService.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CartService.UseCases.Commands
{
    public record DeleteCartCommand(long UserId) : ICommand<Result>;

    public class DeleteCartCommandHandler(
        ICartRepository cartRepository) : ICommandHandler<DeleteCartCommand, Result>
    {
        public async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            await cartRepository.ClearCartAsync(request.UserId);
            return Result.Success();
        }
    }
}
