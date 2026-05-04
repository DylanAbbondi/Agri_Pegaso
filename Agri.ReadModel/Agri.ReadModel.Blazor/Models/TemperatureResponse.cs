namespace Agri.ReadModel.Models;

/// <summary>
/// Represents a temperature reading to be displayed on the dashboard.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor.</param>
/// <param name="Value">The temperature value in Celsius.</param>
/// <param name="Status">The status of the reading (e.g., "Normal", "High").</param>
/// <param name="OccuredOnUtc">The UTC timestamp when the reading was taken.</param>
public record TemperatureResponse(string SensorCode, double Value, string Status, DateTime OccuredOnUtc) : IReading;
