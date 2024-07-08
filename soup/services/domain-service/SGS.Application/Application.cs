using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SGS.Application.Core.Behaviors;

namespace SGS.Application;

public static class Application
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Application).Assembly;
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });
    }
}