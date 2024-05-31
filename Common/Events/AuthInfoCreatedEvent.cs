using Common.Events.Abstractions;

namespace Common.Events;

public class AuthInfoCreatedEvent : EventBase
{
    public string Email { get; init; }
    public string InviteCode { get; init; }

    public AuthInfoCreatedEvent(Guid id, Guid transientId, string email, string inviteCode) : base(id, transientId)
    {
        Email = email;
        InviteCode = inviteCode;
    }
}