using Agri.ReadModel.Models;

namespace Agri.ReadModel.Blazor.Hubs;

/// <summary>
/// Defines the client-side methods that the SignalR hub can invoke.
/// This ensures type safety for client-server communication.
/// </summary>
public interface IDataHubClient
{
    /// <summary>
    /// Receives temperature data on the client.
    /// </summary>
    /// <param name="data">The temperature reading.</param>
    Task ReceiveTemperatureData(TemperatureResponse data);

    /// <summary>
    /// Receives humidity data on the client.
    /// </summary>
    /// <param name="data">The humidity reading.</param>
    Task ReceiveHumidityData(HumidityResponse data);

    /// <summary>
    /// Receives pH data on the client.
    /// </summary>
    /// <param name="data">The pH reading.</param>
    Task ReceivePhData(PhResponse data);
}
