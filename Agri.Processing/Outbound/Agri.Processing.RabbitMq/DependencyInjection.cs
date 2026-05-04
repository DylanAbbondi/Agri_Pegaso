using Agri.Processing.Application.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Agri.Processing.Outbound.RabbitMq;

/// <summary>
/// Extension methods for setting up RabbitMQ outbound processing dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the <see cref="IBusService"/> implementation for publishing messages.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddRabbitMqOutbound(this IServiceCollection services)
    {
        services.AddScoped<IBusService, BusService>();
        return services;
    }
}
