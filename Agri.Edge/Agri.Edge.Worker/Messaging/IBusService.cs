using Agri.Contracts.Abstraction;
using MassTransit;

namespace Agri.Edge.Worker.Messaging;

public interface IBusService
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent;
}

public class BusService(IPublishEndpoint bus)
    : IBusService
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent
    {
        ArgumentNullException.ThrowIfNull(message);

        return bus.Publish(message);
    }
}
