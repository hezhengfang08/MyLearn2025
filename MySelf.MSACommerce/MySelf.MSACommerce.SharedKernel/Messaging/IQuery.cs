

using MediatR;

namespace MySelf.MSACommerce.SharedKernel.Messaging
{
    public interface IQuery<out TReponse> : IRequest<TReponse>
    {
    }
}
