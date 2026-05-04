namespace Agri.ReadModel.Models;

/// <summary>
/// Common interface for sensor readings to facilitate generic calculations.
/// </summary>
public interface IReading
{
    /// <summary>
    /// Gets the sensor value.
    /// </summary>
    double Value { get; }
}
