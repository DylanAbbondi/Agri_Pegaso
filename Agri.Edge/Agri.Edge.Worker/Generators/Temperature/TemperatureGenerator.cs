using Agri.Contracts.V1;
using Agri.Edge.Worker.Messaging;
using Microsoft.Extensions.Hosting;

namespace Agri.Edge.Worker.Generators.Temperature;

public sealed class TemperatureGenerator(
    IBusService bus
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Random randome = new();

        while (!stoppingToken.IsCancellationRequested)
        {
            TemperatureCreatedIntegrationEvent message = new(
                SensorCode: "TEMP001",
                Value: 15 + randome.NextDouble() * 10
                );

            await bus.PublishAsync(message, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}