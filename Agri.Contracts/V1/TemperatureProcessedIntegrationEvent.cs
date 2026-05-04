using Agri.Contracts.Abstraction;

namespace Agri.Contracts.V1;

/// <summary>
/// Represents an integration event published after a temperature reading has been processed.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor that generated the reading.</param>
/// <param name="Value">The processed temperature value.</param>
/// <param name="Status">The status of the sensor after processing the reading (e.g., "Normal", "Alert").</param>
/// <param name="OccurredOnUtc">The timestamp indicating when the processing was completed.</param>
public record TemperatureProcessedIntegrationEvent(string SensorCode, double Value, string Status) : IntegrationEvent;
