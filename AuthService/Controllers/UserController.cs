using System.ComponentModel.DataAnnotations;
using AuthService.Commands;
using AuthService.Publisher;
using AuthService.Repositories;
using AuthService.Requests;
using Common.Events;
using Common.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IEventPublisher<UserCreatedEvent> _eventPublisher;

    public UserController(IUserRepository userRepository, IEventPublisher<UserCreatedEvent> eventPublisher)
    {
        _userRepository = userRepository;
        _eventPublisher = eventPublisher;
    }

    [HttpPost]
    public async Task<IActionResult> Post([Required]CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request, _userRepository, _eventPublisher);

        await command.Execute(cancellationToken);

        return NoContent();
    }
}