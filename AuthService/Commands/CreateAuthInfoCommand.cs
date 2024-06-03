using AuthService.Data;
using Common.Commands;
using Common.Events;
using Common.Publisher;
using Domain.Entities;

namespace AuthService.Commands;

public class CreateAuthInfoCommand : ICommand
{
    private readonly UserCreatedEvent _message;
    private readonly AuthDbContext _ctx;
    private readonly IEventPublisher<AuthInfoCreatedEvent> _publisher;

    public CreateAuthInfoCommand(UserCreatedEvent message, AuthDbContext ctx, IEventPublisher<AuthInfoCreatedEvent> publisher)
    {
        _message = message;
        _ctx = ctx;
        _publisher = publisher;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var authInfo = new AuthInfo(_message.UserId, _message.Email, _message.UserRole);

        _ctx.AuthInfos.Add(authInfo);

        await _ctx.SaveChangesAsync(cancellationToken);

        var message =
            new AuthInfoCreatedEvent(Guid.NewGuid(), _message.TransientId, authInfo.Email, authInfo.InviteCode);

        await _publisher.Publish(message, cancellationToken);
    }
}