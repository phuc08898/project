
namespace SGS.Infrastructure.Common.RabbitMQ.Interfaces;

public interface IEventBusRabbitMQ
{
    void Public<T>(T @event) where T : IntegrationEvent;
    void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;
}