using Common.Commands;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Requests;

namespace UserService.Commands;

public class SelectSubjectCommand : ICommand
{
    private readonly UserDbContext _context;
    private readonly SelectSubjectRequest _request;
    private readonly Guid _studentId;

    public SelectSubjectCommand(UserDbContext context, SelectSubjectRequest request, Guid studentId)
    {
        _context = context;
        _request = request;
        _studentId = studentId;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var student = await _context.Students.SingleAsync(s => s.Id == _studentId, cancellationToken);

        var subject = await _context.Subjects.SingleAsync(s => s.Id == _request.SubjectId, cancellationToken);
        
        student.SelectSubject(subject);

        await _context.SaveChangesAsync(cancellationToken);
    }
}