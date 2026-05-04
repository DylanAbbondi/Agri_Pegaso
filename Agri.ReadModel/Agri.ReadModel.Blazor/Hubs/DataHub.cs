using Microsoft.AspNetCore.SignalR;

namespace Agri.ReadModel.Blazor.Hubs;

/// <summary>
/// SignalR hub for broadcasting real-time sensor data to connected clients.
/// It uses a strongly typed client interface <see cref="IDataHubClient"/>
/// to ensure type safety.
/// </summary>
public class DataHub : Hub<IDataHubClient>
{

}
