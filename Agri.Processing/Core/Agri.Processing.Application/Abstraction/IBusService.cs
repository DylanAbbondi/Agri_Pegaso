using Agri.Contracts.Abstraction;

namespace Agri.Processing.Application.Abstraction;

public interface IBusService
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent;
}
