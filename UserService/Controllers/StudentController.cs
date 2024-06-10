using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.Data;
using UserService.Requests;
using UserService.Responses;

namespace UserService.Controllers;

[ApiController]
[Authorize(Roles = "Student")]
[Route("api/[controller]/[action]")]
public class StudentController : ControllerBase
{
    private readonly UserDbContext _context;

    public StudentController(UserDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> MySubjects(CancellationToken cancellationToken)
    {
        Guid studentId;

        if (User.HasClaim(c => c.Type == ClaimTypeConstants.UserId))
        {
            studentId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypeConstants.UserId).Value);
        }
        else
        {
            return Forbid();
        }
        
        var subjects = await _context.Subjects
            .Where(s => s.Students.Any(st => st.Id == studentId))
            .OrderBy(s => s.Title)
            .Select(s => new SubjectResponse(s.Id, s.Title))
            .ToListAsync(cancellationToken);

        return Ok(subjects);
    }

    [HttpPost]
    public async Task<IActionResult> SelectSubject(SelectSubjectRequest request, CancellationToken cancellationToken)
    {
        Guid studentId;

        if (User.HasClaim(c => c.Type == ClaimTypeConstants.UserId))
        {
            studentId = Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypeConstants.UserId).Value);
        }
        else
        {
            return Forbid();
        }

        try
        {
            var command = new SelectSubjectCommand(_context, request, studentId);

            await command.Execute(cancellationToken);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}