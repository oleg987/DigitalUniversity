using System.ComponentModel.DataAnnotations;
using AuthService.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([Required] LoginRequest request, CancellationToken cancellationToken)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Refresh([Required] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Activate([Required] ActivateUserRequest request, CancellationToken cancellationToken)
    {
        return Ok();
    }
}