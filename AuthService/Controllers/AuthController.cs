using System.ComponentModel.DataAnnotations;
using AuthService.Commands;
using AuthService.Data;
using AuthService.Requests;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly AuthDbContext _context;
    private readonly JwtAuthService _jwtAuthService;

    public AuthController(AuthDbContext context, JwtAuthService jwtAuthService)
    {
        _context = context;
        _jwtAuthService = jwtAuthService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([Required] LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _jwtAuthService.LoginAsync(request, cancellationToken);

            return Ok(token);
        }
        catch
        {
            return Unauthorized();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Refresh([Required] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _jwtAuthService.RefreshAsync(request, cancellationToken);

            return Ok(token);
        }
        catch
        {
            return Unauthorized();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Activate([Required] ActivateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var command = new ActivateUserCommand(_context, request);

            await command.Execute(cancellationToken);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}