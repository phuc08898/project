using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SGS.Infrastructure.Common.RabbitMQ.Interfaces;

namespace SGS.Infrastructure.Common.RabbitMQ;

public class EventBusRabbitMQ(
    IRabbitMQPersistenceConnection rmqConnection,
    IServiceProvider serviceProvider) : IEventBusRabbitMQ
{
    private IModel? _channel;

    public void Public<T>(T @event) where T : IntegrationEvent
    {
        if (_channel == null || !_channel.IsOpen)
        {
            _channel = rmqConnection.IsConnected ? rmqConnection.CreateModel() :
                throw new InvalidOperationException("[Public] No RabbitMQ connection available.");
        }

        var eventName = @event.GetType().Name;
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "",
                              routingKey: eventName,
                              basicProperties: null,
                              body: body);
    }

    public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        if (_channel == null || !_channel.IsOpen)
        {
            _channel = rmqConnection.IsConnected ? rmqConnection.CreateModel() :
                throw new InvalidOperationException("[Subcribe] No RabbitMQ connection available.");
        }

        var eventName = typeof(T).Name;
        _channel.QueueDeclare(queue: eventName,
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);
        // _channel.QueueBind(queue: eventName, exchange: "", routingKey: eventName);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var integrationEvent = JsonSerializer.Deserialize<T>(message);

            var handler = Activator.CreateInstance(typeof(TH)) as IIntegrationEventHandler<T>;
            await handler!.Handle(integrationEvent!, serviceProvider);

            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: eventName,
                             autoAck: false,
                             consumer: consumer);
    }
}
