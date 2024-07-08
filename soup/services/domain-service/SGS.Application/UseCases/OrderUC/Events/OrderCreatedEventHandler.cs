using Microsoft.Extensions.Logging;
using SGS.Common.Domain.Events;
using SGS.Domain.Events;

namespace SGS.Application.UseCases.OrderUC.Events;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEvent> logger) :
    IDomainEventHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("ON Event OrderCreated");
        return Task.CompletedTask;
    }
}