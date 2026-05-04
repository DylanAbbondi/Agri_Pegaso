using Agri.Shared.Result;

namespace Agri.Processing.Domain.Ph;

/// <summary>
/// Provides error constants for ph-related operations.
/// </summary>
public static class PhErrors
{
    /// <summary>
    /// Error returned when the ph percentage is outside the valid range of 0-14.
    /// </summary>
    public static readonly Error InvalidRange =
        Error.Failure("Ph.Invalid", "pH must be between 0 and 14.");
}
