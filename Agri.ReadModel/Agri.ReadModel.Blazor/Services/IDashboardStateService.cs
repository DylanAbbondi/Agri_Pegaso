using Agri.ReadModel.Models;

namespace Agri.ReadModel.Blazor.Services;

/// <summary>
/// Defines the contract for a service that manages the state of the dashboard,
/// including sensor readings and calculated metrics.
/// </summary>
public interface IDashboardStateService
{
    /// <summary>
    /// Gets the most recent temperature reading.
    /// </summary>
    TemperatureResponse? LatestTemperature { get; }

    /// <summary>
    /// Gets the most recent humidity reading.
    /// </summary>
    HumidityResponse? LatestHumidity { get; }

    /// <summary>
    /// Gets the most recent pH reading.
    /// </summary>
    PhResponse? LatestPh { get; }

    /// <summary>
    /// Gets a read-only list of historical temperature readings.
    /// </summary>
    IReadOnlyList<TemperatureResponse> TemperatureReadings { get; }

    /// <summary>
    /// Gets a read-only list of historical humidity readings.
    /// </summary>
    IReadOnlyList<HumidityResponse> HumidityReadings { get; }

    /// <summary>
    /// Gets a read-only list of historical pH readings.
    /// </summary>
    IReadOnlyList<PhResponse> PhReadings { get; }

    /// <summary>
    /// Gets the average temperature.
    /// </summary>
    double TemperatureAverage { get; }

    /// <summary>
    /// Gets the average humidity.
    /// </summary>
    double HumidityAverage { get; }

    /// <summary>
    /// Gets the average pH.
    /// </summary>
    double PhAverage { get; }

    /// <summary>
    /// Gets the moving average for temperature readings.
    /// </summary>
    IReadOnlyList<double> TemperatureMovingAverage { get; }

    /// <summary>
    /// Gets the moving average for humidity readings.
    /// </summary>
    IReadOnlyList<double> HumidityMovingAverage { get; }

    /// <summary>
    /// Gets the moving average for pH readings.
    /// </summary>
    IReadOnlyList<double> PhMovingAverage { get; }

    /// <summary>
    /// Adds a new temperature reading to the state.
    /// </summary>
    /// <param name="temperature">The temperature reading to add.</param>
    void AddTemperatureReading(TemperatureResponse temperature);

    /// <summary>
    /// Adds a new humidity reading to the state.
    /// </summary>
    /// <param name="humidity">The humidity reading to add.</param>
    void AddHumidityReading(HumidityResponse humidity);

    /// <summary>
    /// Adds a new pH reading to the state.
    /// </summary>
    /// <param name="ph">The pH reading to add.</param>
    void AddPhReading(PhResponse ph);
}
