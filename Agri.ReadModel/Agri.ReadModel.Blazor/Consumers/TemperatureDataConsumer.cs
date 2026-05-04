using Agri.Contracts.V1;
using Agri.ReadModel.Blazor.Hubs;
using Agri.ReadModel.Models;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Agri.ReadModel.Blazor.Consumers;

/// <summary>
/// Consumer for processed temperature messages.
/// Listens for integration events from the message queue
/// and forwards the data to the dashboard via SignalR.
/// </summary>
/// <param name="hubContext">The SignalR hub context for communicating with clients.</param>
public class TemperatureDataConsumer(
    IHubContext<DataHub, IDataHubClient> hubContext)
    : IConsumer<TemperatureProcessedIntegrationEvent>
{
    /// <summary>
    /// Processes a <see cref="TemperatureProcessedIntegrationEvent"/> message.
    /// </summary>
    /// <param name="context">The message context.</param>
    public Task Consume(ConsumeContext<TemperatureProcessedIntegrationEvent> context)
    {
        var @event = context.Message;
        var temperature = new TemperatureResponse(
            @event.SensorCode,
            @event.Value,
            @event.Status,
            @event.OccuredOnUtc);

        return hubContext.Clients.All.ReceiveTemperatureData(temperature);
    }
}
