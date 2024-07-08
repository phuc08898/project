using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using SGS.Infrastructure.Common.RabbitMQ.Interfaces;

namespace SGS.Infrastructure.IntegrationService;

public class RabbitMQPersistenceService(
    IConnectionFactory connectionFactory,
    ILogger<RabbitMQPersistenceService> logger,
    int retryCount = 8) : IRabbitMQPersistenceConnection
{
    private IConnectionFactory _connectionFactory = connectionFactory;
    private IConnection? _connection = null;
    private bool _disposed = false;
    private bool _isRegister = false;

    private readonly object syncRoot = new();

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;


    public void Dispose()
    {
        _disposed = true;
        try
        {
            _connection?.Dispose();
            _isRegister = false;
        }
        catch (IOException ex)
        {
            logger.LogCritical(ex.Message);
        }
    }

    public IModel CreateModel()
    {
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMQ connection.");

        return _connection!.CreateModel();
    }

    public bool InitialConnection()
    {
        logger.LogInformation("RabbitMQ client is trying to connect ... ");
        var policy = RetryPolicy.Handle<SocketException>()
                       .Or<BrokerUnreachableException>()
                       .WaitAndRetry(retryCount, retryAttempt
                           => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                               (ex, time) =>
                               {
                                   logger.LogWarning(ex, $"RabbitMQ could not connect after {time}s ({ex.Message}) ");
                               });

        policy.Execute(() =>
        {
            if (_connection == null)
            {
                _connection = _connectionFactory.CreateConnection();
                _disposed = false;
            }

        });

        if (IsConnected)
        {
            if (!_isRegister)
            {
                _connection!.ConnectionShutdown += OnConnectionShutdown;
                _connection!.CallbackException += OnCallbackException;
                _connection!.ConnectionBlocked += OnConnectionBlocked;
                _isRegister = true;
            }

            logger.LogInformation("RabbitMQ server connected.");
            return true;
        }

        logger.LogCritical("FATAL ERROR: RabbitMQ Connection could not be established");
        return false;
    }

    private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;

        logger.LogWarning("A RabbitMQ connection is blocked. Trying to reconnect ... ");

    }

    private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return;

        logger.LogWarning("A RabbitMQ connection is throw exception. Trying to reconnect ... ");

    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        if (_disposed) return;

        logger.LogWarning("A RabbitMQ connection is shutdown. Trying to reconnect ... ");

        var policy = RetryPolicy.HandleResult<bool>(false)
            .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
         (result, time) =>
         {
             logger.LogWarning($"RabbitMQ could not connect after {time}s, retrying ...");
         });

        policy.Execute(InitialConnection);
    }
}
