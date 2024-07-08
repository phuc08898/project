using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SGS.Application.DataTransferObjects;
using SGS.Application.UseCases.OrderUC.Command.UpdateOrderStateCommand;
using SGS.Domain.Enums;
using SGS.Infrastructure.Common.RabbitMQ.Interfaces;

namespace SGS.Infrastructure.RabbitMQ.TransactionEvent.Consumers;

public class RabbitMQTransactionEventHandler :
    IIntegrationEventHandler<RabbitMQTransactionEvent>
{
    public async Task Handle(RabbitMQTransactionEvent @event, IServiceProvider serviceProvider)
    {
        var arg = new UpdateOrderStateArg
        {
            OrderId = @event!.OrderId,
            RequestId = @event!.RequestId,
            State = new OrderStates(OrderStates.COMPLETED)
        };

        await using var scope = serviceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new UpdateOrderStateCommand(arg));
    }
}