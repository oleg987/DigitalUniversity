using AuthService.Publisher;
using AuthService.Repositories;
using AuthService.Requests;
using Common.Events;
using Common.Publisher;
using Domain.Entities;

namespace AuthService.Commands;

public class CreateUserCommand : ICommand
{
    private readonly CreateUserRequest _request;
    private readonly IUserRepository _repository;
    private readonly IEventPublisher<UserCreatedEvent> _publisher;

    public CreateUserCommand(CreateUserRequest request,
        IUserRepository repository,
        IEventPublisher<UserCreatedEvent> publisher)
    {
        _request = request;
        _repository = repository;
        _publisher = publisher;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var user = new User(_request.Id, _request.Name, _request.Email, _request.Role);
        
        await _repository.Add(user, cancellationToken);

        var userCreatedEvent = new UserCreatedEvent(Guid.NewGuid(), Guid.NewGuid(), user.Email, user.InviteCode);
        
        await _publisher.Publish(userCreatedEvent, cancellationToken);
    }
}