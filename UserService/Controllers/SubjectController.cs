using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UserService.Commands;
using UserService.Data;
using UserService.Requests;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SubjectController : ControllerBase
{
    private readonly UserDbContext _context;

    public SubjectController(UserDbContext context)
    {
        _context = context;
    }

    // TODO: get professorId from claims.
    [HttpPost]
    public async Task<IActionResult> Create([Required] CreateSubjectRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateSubjectCommand(_context, request);

            await command.Execute(cancellationToken);
        
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}