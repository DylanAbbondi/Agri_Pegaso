namespace Agri.ReadModel.Models;

/// <summary>
/// Represents a pH reading to be displayed on the dashboard.
/// </summary>
/// <param name="SensorCode">The unique identifier of the sensor.</param>
/// <param name="Value">The pH value.</param>
/// <param name="Status">The status of the reading (e.g., "Normal", "Alkaline").</param>
/// <param name="OccuredOnUtc">The UTC timestamp when the reading was taken.</param>
public record PhResponse(string SensorCode, double Value, string Status, DateTime OccuredOnUtc) : IReading;
