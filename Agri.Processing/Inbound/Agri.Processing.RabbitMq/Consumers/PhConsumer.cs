using Agri.Contracts.V1;
using Agri.Processing.Application.Ph;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Agri.Processing.Inbound.RabbitMq.Consumers;

public sealed class PhConsumer
    (IMediator mediator,
    ILogger<PhConsumer> logger)
    : IConsumer<PhCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<PhCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        var result = await mediator.Send(new ProcessPhCommand(message.SensorCode, message.Value, message.OccuredOnUtc));

        if (result.IsFailure)
        {
            logger.LogWarning(
                "Processing of {MessageName} failed. Error: {Error}. Message will be moved to the error queue.",
                nameof(PhCreatedIntegrationEvent),
                result.Error);

            throw new InvalidOperationException($"Error processing ph event: {result.Error}");
        }
    }
}