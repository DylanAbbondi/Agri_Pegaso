namespace Agri.Processing.Domain.Common;

/// <summary>
/// Represents the status of a sensor based on its latest reading and predefined thresholds.
/// </summary>
public enum SensorStatus
{
    /// <summary>
    /// Indicates that the sensor reading is within the optimal range.
    /// </summary>
    Ok,

    /// <summary>
    /// Indicates that the sensor reading is below the optimal range but not yet critically low.
    /// </summary>
    WarningLow,

    /// <summary>
    /// Indicates that the sensor reading is above the optimal range but not yet critically high.
    /// </summary>
    WarningHigh,

    /// <summary>
    /// Indicates that the sensor reading is critically low and requires attention.
    /// </summary>
    TooLow,

    /// <summary>
    /// Indicates that the sensor reading is critically high and requires attention.
    /// </summary>
    TooHigh
}
