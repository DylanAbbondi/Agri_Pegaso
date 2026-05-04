using Agri.Shared.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Agri.Processing.Inbound.RabbitMq;

/// <summary>
/// Extension methods for setting up RabbitMQ inbound processing dependencies.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds and configures the necessary services for RabbitMQ message consumption.
    /// This method centralizes the setup of MassTransit and related configurations.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The application configuration, used to bind RabbitMQ settings.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddRabbitMqInbound(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.SectionName));

        services.AddMassTransitConfiguration();

        return services;
    }

    /// <summary>
    /// Configures MassTransit with RabbitMQ, including consumers and endpoint details.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    private static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(busConfiguration =>
        {
            busConfiguration.AddConsumers(AssemblyReference.Assembly);

            busConfiguration.SetKebabCaseEndpointNameFormatter();

            busConfiguration.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                configurator.UseMessageRetry(r => r.Exponential(
                    retryLimit: 5,
                    minInterval: TimeSpan.FromSeconds(2),
                    maxInterval: TimeSpan.FromSeconds(30),
                    intervalDelta: TimeSpan.FromSeconds(3))
                );

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
