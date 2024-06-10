using Common.Commands;
using Common.Events;
using Common.Publisher;
using Domain.Entities;
using UserService.Data;
using UserService.Factories;
using UserService.Requests;

namespace UserService.Commands;

public class CreateUserCommand : ICommand
{
    private readonly CreateUserRequest _request;
    private readonly UserDbContext _userDbContext;
    private readonly IEventPublisher<UserCreatedEvent> _publisher;
    private readonly UserFactory _userFactory;

    public CreateUserCommand(CreateUserRequest request,
        UserDbContext userDbContext,
        IEventPublisher<UserCreatedEvent> publisher)
    {
        _request = request;
        _userDbContext = userDbContext;
        _publisher = publisher;
        _userFactory = new UserFactory();
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var user = _userFactory.Create(_request);

        _userDbContext.Users.Add(user);

        await _userDbContext.SaveChangesAsync(cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(Guid.NewGuid(), Guid.NewGuid(),  user.Id, (int)user.Role, user.Email);
        
        await _publisher.Publish(userCreatedEvent, cancellationToken);
    }
}