using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient.Domain.Abstractions
{
    public abstract class BaseEntity
    {

        public Guid Id
        {
            get; set;
        }

        public bool HasActive 
        { 
            get; set; 
        }

        private readonly List<BaseEvent> _domainEvents = new();

        public IReadOnlyCollection<BaseEvent> DomainEvents => this._domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            this._domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            this._domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            this._domainEvents.Clear();
        }
    }
}
