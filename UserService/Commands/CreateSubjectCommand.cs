using Common.Commands;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Requests;

namespace UserService.Commands;

public class CreateSubjectCommand : ICommand
{
    private readonly UserDbContext _ctx;
    private readonly CreateSubjectRequest _request;
    private readonly Guid _professorId;

    public CreateSubjectCommand(UserDbContext ctx, CreateSubjectRequest request, Guid professorId)
    {
        _ctx = ctx;
        _request = request;
        _professorId = professorId;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var professor = await _ctx.Professors.SingleAsync(p => p.Id == _professorId, cancellationToken);

        var subject = new Subject(_request.Id, _request.Title, professor);

        _ctx.Subjects.Add(subject);

        await _ctx.SaveChangesAsync(cancellationToken);
    }
}