using System.Text.Json.Serialization;
using SGS.Common.Domain.Events;
using SGS.Domain.Events;

namespace SGS.Domain.Common.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    [JsonIgnore]
    public ICollection<IDomainEvent> DomainEvents => _domainEvents;
    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}