

namespace SGS.Infrastructure.Common.RabbitMQ.Interfaces;

public interface IIntegrationEventHandler<in T> where T : IntegrationEvent
{
    Task Handle(T @event, IServiceProvider serviceProvider);
    // Task Handle(T @event, IMediator mediator);
}