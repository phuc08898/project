using SGS.Infrastructure.Common.RabbitMQ;

namespace SGS.Infrastructure.RabbitMQ.TransactionEvent.Consumers;
public class RabbitMQTransactionEvent : IntegrationEvent
{
    public int ResultCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public required string OrderId { get; set; }
    public required string RequestId { get; set; }
    public required long TransId { get; set; }
}
