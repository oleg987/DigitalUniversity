using Common.Commands;
using Common.Events;
using Common.Publisher;
using Domain.Entities;
using UserService.Data;
using UserService.Requests;

namespace UserService.Commands;

public class CreateUserCommand : ICommand
{
    private readonly CreateUserRequest _request;
    private readonly UserDbContext _userDbContext;
    private readonly IEventPublisher<UserCreatedEvent> _publisher;

    public CreateUserCommand(CreateUserRequest request,
        UserDbContext userDbContext,
        IEventPublisher<UserCreatedEvent> publisher)
    {
        _request = request;
        _userDbContext = userDbContext;
        _publisher = publisher;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var user = new User(_request.Id, _request.Name, _request.Email, _request.Role);

        _userDbContext.Users.Add(user);

        await _userDbContext.SaveChangesAsync(cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), user.Email, user.InviteCode);
        
        await _publisher.Publish(userCreatedEvent, cancellationToken);
    }
}