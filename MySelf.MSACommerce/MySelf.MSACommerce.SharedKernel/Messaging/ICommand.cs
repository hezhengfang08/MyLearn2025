

using MediatR;

namespace MySelf.MSACommerce.SharedKernel.Messaging
{
    public interface ICommand<out TResponse> :IRequest<TResponse>
    {
    }
}
