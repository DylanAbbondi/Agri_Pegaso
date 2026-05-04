using Agri.Processing.Domain.Common;
using Agri.Shared.Result;

namespace Agri.Processing.Domain.Temperature;

/// <summary>
/// Represents a processed temperature reading, encapsulating the value, sensor identity, and status.
/// This value object ensures that a temperature reading is always in a valid and consistent state.
/// </summary>
public sealed record TemperatureValue
{
    /// <summary>
    /// Gets the unique identifier of the sensor that produced the reading.
    /// </summary>
    public string SensorCode { get; }

    /// <summary>
    /// Gets the temperature reading in Celsius.
    /// </summary>
    public double Celsius { get; }

    /// <summary>
    /// Gets the status of the reading (e.g., Ok, WarningHigh) as evaluated by the threshold policy.
    /// </summary>
    public SensorStatus Status { get; }

    /// <summary>
    /// Gets the Coordinated Universal Time (UTC) timestamp of when the reading occurred.
    /// </summary>
    public DateTime OccuredOnUtc { get; }

    /// <summary>
    /// Defines the thresholds for temperature values in Celsius to determine the sensor status.
    /// </summary>
    private static readonly ThresholdPolicy Policy =
        new(
            Min: 10,
            WarningLow: 15,
            WarningHigh: 30,
            Max: 35
        );

    private TemperatureValue(
        string sensorCode,
        double celsius,
        SensorStatus status,
        DateTime occuredOnUtc)
    {
        SensorCode = sensorCode;
        Celsius = celsius;
        Status = status;
        OccuredOnUtc = occuredOnUtc;
    }

    /// <summary>
    /// Factory method to create a new <see cref="TemperatureValue"/> instance.
    /// It validates the raw temperature value and evaluates its status against the defined policy.
    /// </summary>
    /// <param name="sensorCode">The unique identifier of the sensor.</param>
    /// <param name="celsius">The raw temperature in Celsius.</param>
    /// <param name="occuredOnUtc">The timestamp of the reading.</param>
    /// <returns>A <see cref="Result{T}"/> containing the <see cref="TemperatureValue"/> on success, or an error if validation fails.</returns>
    public static Result<TemperatureValue> Create(
        string sensorCode,
        double celsius,
        DateTime occuredOnUtc)
    {
        if (celsius is < -50 or > 60)
        {
            return Result.Failure<TemperatureValue>(TemperatureErrors.InvalidTemperature);
        }

        var status = Policy.Evaluate(celsius);

        return new TemperatureValue(
            sensorCode,
            celsius,
            status,
            occuredOnUtc);
    }
}
