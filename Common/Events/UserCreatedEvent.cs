using Common.Events.Abstractions;

namespace Common.Events;

public class UserCreatedEvent : EventBase
{
    public string Email { get; init; }
    public string InviteCode { get; init; }

    public UserCreatedEvent(Guid id, Guid transientId, string email, string inviteCode) : base(id, transientId)
    {
        Email = email;
        InviteCode = inviteCode;
    }
}