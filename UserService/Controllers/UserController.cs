using System.ComponentModel.DataAnnotations;
using Common.Events;
using Common.Publisher;
using Common.Settings;
using Microsoft.AspNetCore.Mvc;
using UserService.Commands;
using UserService.Data;
using UserService.Requests;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly UserDbContext _ctx;
    private readonly IEventPublisher<UserCreatedEvent> _publisher;

    public UserController(UserDbContext ctx, IEventPublisher<UserCreatedEvent> publisher)
    {
        _ctx = ctx;
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([Required] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request, _ctx, _publisher);

        await command.Execute(cancellationToken);

        return NoContent();
    }
}