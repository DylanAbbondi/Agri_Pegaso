using Agri.Contracts.V1;
using Agri.Processing.Application.Abstraction;
using Agri.Processing.Application.Common;
using Agri.Processing.Domain.Temperature;
using Agri.Shared.Result;

namespace Agri.Processing.Application.Temperature;

/// <summary>
/// Handles the command to process a raw temperature reading.
/// This involves validating the reading, applying business rules via the domain model,
/// and publishing a processed event.
/// </summary>
/// <param name="busService">The service for publishing messages to the bus.</param>
public sealed class ProcessTemperatureCommandHandler(IBusService busService)
    : ICommandHandler<ProcessTemperatureCommand>
{
    /// <summary>
    /// Executes the temperature processing command.
    /// </summary>
    /// <param name="request">The command containing the temperature reading details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous handling operation, returning a <see cref="Result"/> indicating success or failure.</returns>
    public async Task<Result> Handle(ProcessTemperatureCommand request, CancellationToken cancellationToken) =>
      TemperatureValue.Create(request.SensorCode, request.Value, request.OccuredOnUtc)
          .Map(Create)
          .Tap(e => busService.PublishAsync(e, cancellationToken));

    private TemperatureProcessedIntegrationEvent Create(TemperatureValue temperature) =>
        new(temperature.SensorCode,
            temperature.Celsius,
            temperature.Status.ToString());
}
