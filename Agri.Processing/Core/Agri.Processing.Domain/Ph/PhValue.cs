using Agri.Processing.Domain.Common;
using Agri.Shared.Result;

namespace Agri.Processing.Domain.Ph;

/// <summary>
/// Represents a processed pH reading, encapsulating the value, sensor identity, and status.
/// This value object ensures that a pH reading is always in a valid and consistent state.
/// </summary>
public sealed record PhValue
{
    /// <summary>
    /// Gets the unique identifier of the sensor that produced the reading.
    /// </summary>
    public string SensorCode { get; }

    /// <summary>
    /// Gets the pH reading (0-14).
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Gets the status of the reading (e.g., Ok, WarningLow) as evaluated by the threshold policy.
    /// </summary>
    public SensorStatus Status { get; }

    /// <summary>
    /// Gets the Coordinated Universal Time (UTC) timestamp of when the reading occurred.
    /// </summary>
    public DateTime OccuredOnUtc { get; }

    /// <summary>
    /// Defines the thresholds for pH values to determine the sensor status.
    /// </summary>
    private static readonly ThresholdPolicy Policy =
        new(
            Min: 5.5,
            WarningLow: 6.0,
            WarningHigh: 7.5,
            Max: 8.0
        );

    private PhValue(
        string sensorCode,
        double value,
        SensorStatus status,
        DateTime occuredOnUtc)
    {
        SensorCode = sensorCode;
        Value = value;
        Status = status;
        OccuredOnUtc = occuredOnUtc;
    }

    /// <summary>
    /// Factory method to create a new <see cref="PhValue"/> instance.
    /// It validates the raw pH value and evaluates its status against the defined policy.
    /// </summary>
    /// <param name="sensorCode">The unique identifier of the sensor.</param>
    /// <param name="value">The raw pH value (0-14).</param>
    /// <param name="occuredOnUtc">The timestamp of the reading.</param>
    /// <returns>A <see cref="Result{T}"/> containing the <see cref="PhValue"/> on success, or an error if validation fails.</returns>
    public static Result<PhValue> Create(
        string sensorCode,
        double value,
        DateTime occuredOnUtc)
    {
        if (value is < 0 or > 14)
        {
            return Result.Failure<PhValue>(PhErrors.InvalidRange);
        }

        var status = Policy.Evaluate(value);

        return new PhValue(
            sensorCode,
            value,
            status,
            occuredOnUtc);
    }
}
