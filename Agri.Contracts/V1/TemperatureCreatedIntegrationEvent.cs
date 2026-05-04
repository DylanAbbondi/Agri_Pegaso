using Agri.Contracts.Abstraction;

namespace Agri.Contracts.V1;

/// <summary>
/// Represents an integration event that is published when a new temperature reading is created.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor that generated the reading.</param>
/// <param name="Value">The temperature value, typically in Celsius.</param>
public record TemperatureCreatedIntegrationEvent(string SensorCode, double Value) : IntegrationEvent;
