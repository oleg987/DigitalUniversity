using Common.Commands;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Requests;

namespace UserService.Commands;

public class SelectSubjectCommand : ICommand
{
    private readonly UserDbContext _context;
    private readonly SelectSubjectRequest _request;

    public SelectSubjectCommand(UserDbContext context, SelectSubjectRequest request)
    {
        _context = context;
        _request = request;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var student = await _context.Students.SingleAsync(s => s.Id == _request.StudentId, cancellationToken);

        var subject = await _context.Subjects.SingleAsync(s => s.Id == _request.SubjectId, cancellationToken);
        
        student.SelectSubject(subject);

        await _context.SaveChangesAsync(cancellationToken);
    }
}