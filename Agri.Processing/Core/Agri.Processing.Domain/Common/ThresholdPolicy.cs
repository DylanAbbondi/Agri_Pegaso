namespace Agri.Processing.Domain.Common;

/// <summary>
/// Defines a policy for evaluating a sensor value against a set of thresholds.
/// </summary>
/// <param name="Min">The minimum acceptable value before the status becomes 'TooLow'.</param>
/// <param name="WarningLow">The lower bound of the optimal range. Values below this are 'WarningLow'.</param>
/// <param name="WarningHigh">The upper bound of the optimal range. Values above this are 'WarningHigh'.</param>
/// <param name="Max">The maximum acceptable value before the status becomes 'TooHigh'.</param>
public sealed record ThresholdPolicy(
    double Min,
    double WarningLow,
    double WarningHigh,
    double Max)
{
    /// <summary>
    /// Evaluates the given value against the policy thresholds.
    /// </summary>
    /// <param name="value">The sensor value to evaluate.</param>
    /// <returns>The <see cref="SensorStatus"/> corresponding to the value.</returns>
    public SensorStatus Evaluate(double value) =>
        value switch
        {
            _ when value < Min => SensorStatus.TooLow,
            _ when value > Max => SensorStatus.TooHigh,
            _ when value < WarningLow => SensorStatus.WarningLow,
            _ when value > WarningHigh => SensorStatus.WarningHigh,
            _ => SensorStatus.Ok
        };
}

