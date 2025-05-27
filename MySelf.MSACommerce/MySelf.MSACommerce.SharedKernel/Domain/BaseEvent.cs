

using MediatR;

namespace MySelf.MSACommerce.SharedKernel.Domain
{
    public abstract class BaseEvent:INotification
    {
        public DateTimeOffset DataOccurred { get; protected set; } = DateTimeOffset.Now;
    }

}
