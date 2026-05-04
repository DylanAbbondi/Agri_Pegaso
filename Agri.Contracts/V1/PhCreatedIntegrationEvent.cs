using Agri.Contracts.Abstraction;

namespace Agri.Contracts.V1;

/// <summary>
/// Represents an integration event that is published when a new pH reading is created.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor that generated the reading.</param>
/// <param name="Value">The pH value, typically ranging from 0 to 14.</param>
public record PhCreatedIntegrationEvent(string SensorCode, double Value) : IntegrationEvent;
