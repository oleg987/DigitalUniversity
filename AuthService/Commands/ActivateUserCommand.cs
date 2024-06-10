using AuthService.Data;
using AuthService.Requests;
using Common.Commands;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Commands;

public class ActivateUserCommand : ICommand
{
    private readonly AuthDbContext _context;
    private readonly ActivateUserRequest _request;

    public ActivateUserCommand(AuthDbContext context, ActivateUserRequest request)
    {
        _context = context;
        _request = request;
    }

    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var user = await _context.AuthInfos
            .SingleAsync(u => u.InviteCode == _request.InviteCode, cancellationToken);
        
        user.Activate(_request.Password);

        await _context.SaveChangesAsync(cancellationToken);
    }
}