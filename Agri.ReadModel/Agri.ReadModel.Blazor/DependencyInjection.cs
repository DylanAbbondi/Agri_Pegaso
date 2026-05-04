using Agri.Shared.Settings;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Agri.ReadModel.Blazor;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRabbitMqReadModel(configuration);

        return services;
    }

    public static IServiceCollection AddRabbitMqReadModel(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.SectionName));

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
