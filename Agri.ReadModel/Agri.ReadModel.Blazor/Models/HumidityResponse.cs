namespace Agri.ReadModel.Models;

/// <summary>
/// Represents a humidity reading to be displayed on the dashboard.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor.</param>
/// <param name="Value">The humidity value as a percentage.</param>
/// <param name="Status">The status of the reading (e.g., "Normal", "Dry").</param>
/// <param name="OccuredOnUtc">The UTC timestamp when the reading was taken.</param>
public record HumidityResponse(string SensorCode, double Value, string Status, DateTime OccuredOnUtc) : IReading;
