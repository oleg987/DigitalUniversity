using System.ComponentModel.DataAnnotations;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.Data;
using UserService.Requests;
using UserService.Responses;

namespace UserService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class SubjectController : ControllerBase
{
    private readonly UserDbContext _context;

    public SubjectController(UserDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Create([Required] CreateSubjectRequest request,
        CancellationToken cancellationToken)
    {
        Guid professorId;

        if (User.HasClaim(c => c.Type == ClaimTypeConstants.UserId))
        {
            professorId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypeConstants.UserId).Value);
        }
        else
        {
            return Forbid();
        }
        
        try
        {
            var command = new CreateSubjectCommand(_context, request, professorId);

            await command.Execute(cancellationToken);
        
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var subjects = await _context.Subjects
            .OrderBy(s => s.Title)
            .Select(s => new SubjectResponse(s.Id, s.Title))
            .ToListAsync(cancellationToken);

        return Ok(subjects);
    }
}