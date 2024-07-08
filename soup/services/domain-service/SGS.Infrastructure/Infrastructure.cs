using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGS.Infrastructure.Persistence;
using SGS.Infrastructure.Common.Data;
using SGS.Infrastructure.IntegrationService;
using SGS.Application.IIntegrationService;
using SGS.Infrastructure.Core.Mapping;

namespace SGS.Infrastructure;

public static class Infrastructure
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySql");
        services.AddDbContextPool<SGSDbContext>(options => options
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            , poolSize: 1024);

        services.AddScoped<IUnitOfWork, SGSDbContext>();
        services.AddScoped<IPaymentRPCService, PaymentRPCService>();

        // services.AddHostedService<RMQHostedService>();
        // services.AddSingleton<IConnectionFactory>(provider =>
        //    {
        //        return new ConnectionFactory()
        //        {
        //            HostName = configuration["RabbitMQ:Host"] ?? "localhost",
        //            UserName = configuration["RabbitMQ:User"] ?? "guest",
        //            Password = configuration["RabbitMQ:Pass"] ?? "guest",
        //        };
        //    });
        // services.AddSingleton<IRabbitMQPersistenceConnection, RabbitMQPersistenceService>();
        // services.AddSingleton<IEventBusRabbitMQ, EventBusRabbitMQ>();

        services.RegisterMapsterConfiguration();
    }
}
