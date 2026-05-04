using Agri.Contracts.Abstraction;

namespace Agri.Contracts.V1;

/// <summary>
/// Represents an integration event that is published when a new humidity reading is created.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor that generated the reading.</param>
/// <param name="Value">The humidity value, typically expressed as a percentage.</param>
public record HumidityCreatedIntegrationEvent(string SensorCode, double Value) : IntegrationEvent;
