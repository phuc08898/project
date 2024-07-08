using MediatR;

namespace SGS.Common.Domain.Events;
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
{
}