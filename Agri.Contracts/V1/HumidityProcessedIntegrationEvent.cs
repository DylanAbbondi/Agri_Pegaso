using Agri.Contracts.Abstraction;

namespace Agri.Contracts.V1;

/// <summary>
/// Represents an integration event published after a humidity reading has been processed.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor that generated the reading.</param>
/// <param name="Value">The processed humidity value.</param>
/// <param name="Status">The status of the sensor after processing the reading (e.g., "Normal", "Alert").</param>
public record HumidityProcessedIntegrationEvent(string SensorCode, double Value, string Status) : IntegrationEvent;
