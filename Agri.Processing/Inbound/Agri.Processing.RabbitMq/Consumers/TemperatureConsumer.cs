using Agri.Contracts.V1;
using Agri.Processing.Application.Temperature;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Agri.Processing.Inbound.RabbitMq.Consumers;

public sealed class TemperatureConsumer
    (IMediator mediator,
    ILogger<TemperatureConsumer> logger)
    : IConsumer<TemperatureCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<TemperatureCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var result = await mediator.Send(new ProcessTemperatureCommand(message.SensorCode, message.Value, message.OccuredOnUtc));

        if (result.IsFailure)
        {
            logger.LogWarning(
                "Processing of {MessageName} failed. Error: {Error}",
                nameof(TemperatureCreatedIntegrationEvent),
                result.Error);

            throw new InvalidOperationException($"Error processing temperature event: {result.Error}");
        }
    }
}