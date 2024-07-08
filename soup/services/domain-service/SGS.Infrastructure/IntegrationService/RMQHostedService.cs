using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGS.Infrastructure.Common.RabbitMQ.Interfaces;
using SGS.Infrastructure.RabbitMQ.TransactionEvent.Consumers;

namespace SGS.Infrastructure.IntegrationService;

public class RMQHostedService(
    IEventBusRabbitMQ eventBusRabbit,
    IRabbitMQPersistenceConnection rabbitMQPersistenceConnection) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        rabbitMQPersistenceConnection.InitialConnection(); // lock thread and retry forever ... 
        eventBusRabbit.Subscribe<RabbitMQTransactionEvent, RabbitMQTransactionEventHandler>();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        rabbitMQPersistenceConnection.Dispose();
        return Task.CompletedTask;
    }
}

// public class RMQTransactionService(
//     IConfiguration configuration,
//     IServiceProvider serviceProvider,
//     ILogger<RMQTransactionService> logger) : RabbitMQModule(
//     configuration.GetConnectionString("RabbitHost") ?? "localhost"), IHostedService
// {
//     public override async void HandleMessage(object? model, BasicDeliverEventArgs ea)
//     {
//         var body = ea.Body.ToArray();
//         var msg = Encoding.UTF8.GetString(body);
//         logger.LogInformation(msg);

//         var parseMsg = JsonSerializer.Deserialize<RabbitOrderReceiveMessage>(msg);

//         var arg = new UpdateOrderStateArg
//         {
//             OrderId = parseMsg!.OrderId,
//             RequestId = parseMsg!.RequestId,
//             State = new OrderStates(OrderStates.COMPLETED)
//         };

//         await using (var scope = serviceProvider.CreateAsyncScope())
//         {
//             var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

//             await mediator.Send(new UpdateOrderStateCommand(arg));
//         }

//         base.HandleMessage(model, ea);
//     }

//     public Task StartAsync(CancellationToken cancellationToken)
//     {
//         try
//         {
//             this.InitializationConnection(); // throw if fail
//             this.AddQueue("transaction");
//             this.StartListening("transaction");
//             logger.LogInformation("[RabbitMQ] Start Qtransaction listening ...");
//         }
//         catch (BrokerUnreachableException ex)
//         {
//             logger.LogWarning("[RabbitMQ] " + ex.Message);
//         }
//         return Task.CompletedTask;
//     }

//     public Task StopAsync(CancellationToken cancellationToken)
//     {
//         if (this.IsConnected)
//         {
//             this.CloseConnection();
//             logger.LogInformation("[RabbitMQ] Stopped service!");
//         }
//         return Task.CompletedTask;
//     }
// }