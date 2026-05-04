using Agri.Contracts.V1;
using Agri.Edge.Worker.Messaging;
using Microsoft.Extensions.Hosting;

namespace Agri.Edge.Worker.Generators.Humidity;

public sealed class HumidityGenerator(
    IBusService busService
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Random random = new();
        while (!stoppingToken.IsCancellationRequested)
        {
            HumidityCreatedIntegrationEvent message = new(
                SensorCode: "HUM001",
                Value: 30 + random.NextDouble() * 50
                ); // 30–80%

            await busService.PublishAsync(message, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}