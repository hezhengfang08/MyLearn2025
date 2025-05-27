using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedKernel.Domain
{
    public abstract class BaseEntity<Tid>:IEntity<Tid>
    {
        private readonly List<BaseEvent> _domainEvents = [];

        [NotMapped] public IReadOnlyCollection<BaseEvent> DemainEvents => _domainEvents.AsReadOnly();
        public Tid Id { get; set; } = default!;
        public void AddDomainEvent(BaseEvent domainEvents)
        {
            _domainEvents.Add(domainEvents);
        }
        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
