namespace AuthService.Events;

public record UserCreatedEvent(Guid Id, string Email, string InviteCode);