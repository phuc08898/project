using RabbitMQ.Client;

namespace SGS.Infrastructure.Common.RabbitMQ.Interfaces;

public interface IRabbitMQPersistenceConnection
{
    bool IsConnected { get; }
    bool InitialConnection();
    IModel CreateModel();
    void Dispose();
}