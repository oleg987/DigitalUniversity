namespace Common.Events.Abstractions;

/// <summary>
/// Base class for application events.
/// </summary>
public abstract class EventBase
{
    /// <summary>
    /// Event identifier.
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Event flow identifier.
    /// </summary>
    public Guid TransientId { get; init; }

    protected EventBase(Guid id, Guid transientId)
    {
        Id = id;
        TransientId = transientId;
    }
}