using Agri.Contracts.V1;
using Agri.Edge.Worker.Messaging;
using Microsoft.Extensions.Hosting;

namespace Agri.Edge.Worker.Generators.Ph;

public sealed class PhGenerator(
    IBusService busService
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Random random = new();
        while (!stoppingToken.IsCancellationRequested)
        {
            PhCreatedIntegrationEvent message = new(
                SensorCode: "PH001",
                Value: 5 + random.NextDouble() * 4 // pH 5.0–9.0
                );

            await busService.PublishAsync(message, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
