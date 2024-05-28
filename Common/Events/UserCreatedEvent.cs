using Common.Events.Abstractions;

namespace Common.Events;

public class UserCreatedEvent : EventBase
{
    public Guid UserId { get; init; }
    public int UserRole { get; init; } // TODO: ??? Don`t want add reference from Common to Domain or vise versa. 
    public string Email { get; init; }

    public UserCreatedEvent(Guid id, Guid transientId, Guid userId, int userRole, string email) : base(id, transientId)
    {
        UserId = userId;
        UserRole = userRole;
        Email = email;
    }
}