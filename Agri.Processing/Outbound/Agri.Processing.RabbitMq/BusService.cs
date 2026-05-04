using Agri.Contracts.Abstraction;
using Agri.Processing.Application.Abstraction;
using MassTransit;

namespace Agri.Processing.Outbound.RabbitMq;

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
