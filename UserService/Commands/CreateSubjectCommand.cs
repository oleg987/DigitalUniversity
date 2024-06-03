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

    public CreateSubjectCommand(UserDbContext ctx, CreateSubjectRequest request)
    {
        _ctx = ctx;
        _request = request;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var professor = await _ctx.Professors.SingleAsync(p => p.Id == _request.ProfessorId, cancellationToken);

        var subject = new Subject(_request.Id, _request.Title, professor);

        _ctx.Subjects.Add(subject);

        await _ctx.SaveChangesAsync(cancellationToken);
    }
}