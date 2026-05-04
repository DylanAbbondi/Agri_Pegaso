using Agri.Processing.Domain.Common;
using Agri.Shared.Result;

namespace Agri.Processing.Domain.Humidity;

/// <summary>
/// Represents a processed humidity reading, encapsulating the value, sensor identity, and status.
/// This value object ensures that a humidity reading is always in a valid and consistent state.
/// </summary>
public sealed record HumidityValue
{
    /// <summary>
    /// Gets the unique identifier of the sensor that produced the reading.
    /// </summary>
    public string SensorCode { get; }

    /// <summary>
    /// Gets the humidity reading as a percentage (0-100).
    /// </summary>
    public double Percentage { get; }

    /// <summary>
    /// Gets the status of the reading (e.g., Ok, WarningLow) as evaluated by the threshold policy.
    /// </summary>
    public SensorStatus Status { get; }

    /// <summary>
    /// Gets the Coordinated Universal Time (UTC) timestamp of when the reading occurred.
    /// </summary>
    public DateTime OccuredOnUtc { get; }

    /// <summary>
    /// Defines the thresholds for humidity values to determine the sensor status.
    /// </summary>
    private static readonly ThresholdPolicy Policy =
        new(
            Min: 30,
            WarningLow: 40,
            WarningHigh: 70,
            Max: 80
        );

    private HumidityValue(
        string sensorCode,
        double percentage,
        SensorStatus status,
        DateTime occuredOnUtc)
    {
        SensorCode = sensorCode;
        Percentage = percentage;
        Status = status;
        OccuredOnUtc = occuredOnUtc;
    }

    /// <summary>
    /// Factory method to create a new <see cref="HumidityValue"/> instance.
    /// It validates the raw humidity percentage and evaluates its status against the defined policy.
    /// </summary>
    /// <param name="sensorCode">The unique identifier of the sensor.</param>
    /// <param name="percentage">The raw humidity percentage (0-100).</param>
    /// <param name="occuredOnUtc">The timestamp of the reading.</param>
    /// <returns>A <see cref="Result{T}"/> containing the <see cref="HumidityValue"/> on success, or an error if validation fails.</returns>
    public static Result<HumidityValue> Create(
        string sensorCode,
        double percentage,
        DateTime occuredOnUtc)
    {
        if (percentage is < 0 or > 100)
        {
            return Result.Failure<HumidityValue>(HumidityErrors.InvalidPercentage);
        }

        var status = Policy.Evaluate(percentage);

        return new HumidityValue(
            sensorCode,
            percentage,
            status,
            occuredOnUtc);
    }
}
