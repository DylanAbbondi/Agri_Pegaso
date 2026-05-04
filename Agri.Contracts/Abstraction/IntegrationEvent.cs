namespace Agri.Contracts.Abstraction;

/// <summary>
/// Represents a base class for integration events within the system.
/// </summary>
/// <remarks>
/// Integration events are used to communicate state changes between different microservices or bounded contexts.
/// This class provides common properties for all integration events, such as a unique identifier and the creation timestamp.
/// </remarks>
public record class IntegrationEvent
{
    /// <summary>
    /// Gets the unique identifier for the integration event.
    /// </summary>
    public Guid EventId => Guid.NewGuid();

    /// <summary>
    /// Gets the Coordinated Universal Time (UTC) timestamp when the event occurred.
    /// </summary>
    public DateTime OccuredOnUtc => DateTime.UtcNow;
}