using AuthService.Entities;
using AuthService.Events;
using AuthService.Publisher;
using AuthService.Repositories;

namespace AuthService.Commands;

public class RegisterUserCommand : ICommand
{
    private Guid _id;
    private string _email;
    private string _name;
    private UserRole _role;
    private readonly IUserRepository _repository;
    private readonly IEventPublisher<UserCreatedEvent> _publisher;

    public RegisterUserCommand(Guid id,
        string email,
        string name,
        UserRole role,
        IUserRepository repository,
        IEventPublisher<UserCreatedEvent> publisher)
    {
        _id = id;
        _email = email;
        _name = name;
        _role = role;
        _repository = repository;
        _publisher = publisher;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}