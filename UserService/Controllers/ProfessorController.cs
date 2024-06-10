using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Responses;

namespace UserService.Controllers;

[ApiController]
[Authorize(Roles = "Professor")]
[Route("api/[controller]/[action]")]
public class ProfessorController : ControllerBase
{
    private readonly UserDbContext _context;

    public ProfessorController(UserDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> MySubjects(CancellationToken cancellationToken)
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
        
        var subjects = await _context.Subjects
            .Where(s => s.Professor.Id == professorId)
            .OrderBy(s => s.Title)
            .Select(s => new SubjectResponse(s.Id, s.Title))
            .ToListAsync(cancellationToken);

        return Ok(subjects);
    }
}