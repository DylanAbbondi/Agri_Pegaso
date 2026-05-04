using Agri.Shared.Result;

namespace Agri.Processing.Domain.Temperature;

/// <summary>
/// Provides error constants for temperature-related operations.
/// </summary>
public static class TemperatureErrors
{
    /// <summary>
    /// Error returned when the ph percentage is outside the valid range of -50 to 60°.
    /// </summary>
    public static readonly Error InvalidTemperature =
        Error.Failure("Temperature.Invalid", "Temperature must be within physical sensor limits (-50 to 60°C).");
}
