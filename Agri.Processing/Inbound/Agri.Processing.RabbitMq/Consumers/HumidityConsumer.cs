using Agri.Contracts.V1;
using Agri.Processing.Application.Humidity;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Agri.Processing.Inbound.RabbitMq.Consumers;

public sealed class HumidityConsumer
    (IMediator mediator,
    ILogger<HumidityConsumer> logger)
    : IConsumer<HumidityCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<HumidityCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var result = await mediator.Send(new ProcessHumidityCommand(message.SensorCode, message.Value, message.OccuredOnUtc));

        if (result.IsFailure)
        {
            logger.LogWarning(
                "Processing of {MessageName} failed. Error: {Error}. Message will be moved to the error queue.",
                nameof(HumidityCreatedIntegrationEvent),
                result.Error);

            throw new InvalidOperationException($"Error processing humidity event: {result.Error}");
        }
    }
}