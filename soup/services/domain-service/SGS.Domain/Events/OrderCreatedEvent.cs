using SGS.Common.Domain.Events;
using SGS.Domain.Entities;

namespace SGS.Domain.Events;

public sealed class OrderCreatedEvent(Order order) : IDomainEvent
{
    public Order Order { get; set; } = order;
}