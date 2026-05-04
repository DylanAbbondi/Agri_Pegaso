using Agri.Contracts.V1;
using Agri.Processing.Application.Abstraction;
using Agri.Processing.Application.Common;
using Agri.Processing.Domain.Ph;
using Agri.Shared.Result;

namespace Agri.Processing.Application.Ph;

/// <summary>
/// Handles the command to process a raw pH reading.
/// This involves validating the reading, applying business rules via the domain model,
/// and publishing a processed event.
/// </summary>
/// <param name="busService">The service for publishing messages to the bus.</param>
public sealed class ProcessPhCommandHandler(IBusService busService)
    : ICommandHandler<ProcessPhCommand>
{
    /// <summary>
    /// Executes the pH processing command.
    /// </summary>
    /// <param name="request">The command containing the pH reading details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous handling operation, returning a <see cref="Result"/> indicating success or failure.</returns>
    public async Task<Result> Handle(ProcessPhCommand request, CancellationToken cancellationToken) =>
      PhValue.Create(request.SensorCode, request.Value, request.OccuredOnUtc)
          .Map(Create)
          .Tap(e => busService.PublishAsync(e, cancellationToken));

    private PhProcessedIntegrationEvent Create(PhValue ph) =>
        new(ph.SensorCode,
            ph.Value,
            ph.Status.ToString());
}
