using Agri.ReadModel.Models;

namespace Agri.ReadModel.Blazor.Services;

/// <summary>
/// Manages the state of the dashboard, including sensor readings and calculated metrics.
/// This service acts as a singleton to hold the application's UI state.
/// This implementation is thread-safe to handle concurrent updates from message consumers
/// and reads from the UI.
/// </summary>
public class DashboardStateService : IDashboardStateService
{
    private const int MaxReadings = 20;
    private const int MovingAverageWindow = 5;

    // Lock object to ensure thread-safe access to the reading lists.
    private readonly object _lock = new();

    private readonly List<TemperatureResponse> _temperatureReadings = new();
    private readonly List<HumidityResponse> _humidityReadings = new();
    private readonly List<PhResponse> _phReadings = new();

    public TemperatureResponse? LatestTemperature
    {
        get
        {
            lock (_lock)
            {
                return _temperatureReadings.LastOrDefault();
            }
        }
    }

    public HumidityResponse? LatestHumidity
    {
        get
        {
            lock (_lock)
            {
                return _humidityReadings.LastOrDefault();
            }
        }
    }

    public PhResponse? LatestPh
    {
        get
        {
            lock (_lock)
            {
                return _phReadings.LastOrDefault();
            }
        }
    }

    public IReadOnlyList<TemperatureResponse> TemperatureReadings
    {
        get
        {
            lock (_lock)
            {
                // Return a copy to prevent exceptions if the collection is modified while being enumerated by the UI.
                return _temperatureReadings.ToList();
            }
        }
    }

    public IReadOnlyList<HumidityResponse> HumidityReadings
    {
        get
        {
            lock (_lock)
            {
                return _humidityReadings.ToList();
            }
        }
    }

    public IReadOnlyList<PhResponse> PhReadings
    {
        get
        {
            lock (_lock)
            {
                return _phReadings.ToList();
            }
        }
    }

    public double TemperatureAverage { get; private set; }
    public double HumidityAverage { get; private set; }
    public double PhAverage { get; private set; }

    public IReadOnlyList<double> TemperatureMovingAverage { get; private set; } = new List<double>();
    public IReadOnlyList<double> HumidityMovingAverage { get; private set; } = new List<double>();
    public IReadOnlyList<double> PhMovingAverage { get; private set; } = new List<double>();

    /// <summary>
    /// Adds a new temperature reading, trims the collection, and recalculates metrics in a thread-safe manner.
    /// </summary>
    /// <param name="reading">The new temperature reading.</param>
    public void AddTemperatureReading(TemperatureResponse reading)
    {
        lock (_lock)
        {
            AddReading(_temperatureReadings, reading);
            TemperatureAverage = CalculateAverage(_temperatureReadings);
            TemperatureMovingAverage = CalculateMovingAverage(_temperatureReadings);
        }
    }

    /// <summary>
    /// Adds a new humidity reading, trims the collection, and recalculates metrics in a thread-safe manner.
    /// </summary>
    /// <param name="reading">The new humidity reading.</param>
    public void AddHumidityReading(HumidityResponse reading)
    {
        lock (_lock)
        {
            AddReading(_humidityReadings, reading);
            HumidityAverage = CalculateAverage(_humidityReadings);
            HumidityMovingAverage = CalculateMovingAverage(_humidityReadings);
        }
    }

    /// <summary>
    /// Adds a new pH reading, trims the collection, and recalculates metrics in a thread-safe manner.
    /// </summary>
    /// <param name="reading">The new pH reading.</param>
    public void AddPhReading(PhResponse reading)
    {
        lock (_lock)
        {
            AddReading(_phReadings, reading);
            PhAverage = CalculateAverage(_phReadings);
            PhMovingAverage = CalculateMovingAverage(_phReadings);
        }
    }

    /// <summary>
    /// Adds a reading to a list and ensures the list does not exceed the maximum size.
    /// This method is private and should only be called from within a lock.
    /// </summary>
    private void AddReading<T>(List<T> readings, T newReading)
    {
        readings.Add(newReading);
        if (readings.Count > MaxReadings)
        {
            readings.RemoveAt(0);
        }
    }

    /// <summary>
    /// Calculates the average value for a list of readings.
    /// This method is private and should only be called from within a lock.
    /// </summary>
    private double CalculateAverage<T>(List<T> readings) where T : IReading
    {
        if (!readings.Any()) return 0;
        return Math.Round(readings.Average(r => r.Value), 2);
    }

    /// <summary>
    /// Calculates the moving average for a list of readings.
    /// This method is private and should only be called from within a lock.
    /// </summary>
    private IReadOnlyList<double> CalculateMovingAverage<T>(List<T> readings) where T : IReading
    {
        var values = readings.Select(r => r.Value).ToList();
        return values.Select((_, index) =>
        {
            var window = values.Skip(Math.Max(0, index - MovingAverageWindow + 1)).Take(MovingAverageWindow);
            return Math.Round(window.Average(), 2);
        }).ToList();
    }
}
