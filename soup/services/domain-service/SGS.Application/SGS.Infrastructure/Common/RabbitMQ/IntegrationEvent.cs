namespace SGS.Infrastructure.Common.RabbitMQ;

public abstract class IntegrationEvent
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N");
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}