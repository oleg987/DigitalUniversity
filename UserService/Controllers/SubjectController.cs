using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Commands;
using UserService.Data;
using UserService.Requests;
using UserService.Responses;

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

    [HttpGet("{studentId:guid}")]
    public async Task<IActionResult> ByStudentId([Required] Guid studentId, CancellationToken cancellationToken)
    {
        var subjects = await _context.Subjects
            .Where(s => s.Students.Any(st => st.Id == studentId))
            .OrderBy(s => s.Title)
            .Select(s => new SubjectResponse(s.Id, s.Title))
            .ToListAsync(cancellationToken);

        return Ok(subjects);
    }
    
    [HttpGet("{professorId:guid}")]
    public async Task<IActionResult> ByProfessorId([Required] Guid professorId, CancellationToken cancellationToken)
    {
        var subjects = await _context.Subjects
            .Where(s => s.Professor.Id == professorId)
            .OrderBy(s => s.Title)
            .Select(s => new SubjectResponse(s.Id, s.Title))
            .ToListAsync(cancellationToken);

        return Ok(subjects);
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