using Agri.Shared.Result;

namespace Agri.Processing.Domain.Humidity;

/// <summary>
/// Provides error constants for humidity-related operations.
/// </summary>
public static class HumidityErrors
{
    /// <summary>
    /// Error returned when the humidity percentage is outside the valid range of 0-100.
    /// </summary>
    public static readonly Error InvalidPercentage =
        Error.Failure("Humidity.InvalidPercentage", "Humidity must be between 0 and 100.");
}
