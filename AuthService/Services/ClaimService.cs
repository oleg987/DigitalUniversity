using System.Security.Claims;
using AuthService.Data;
using Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services;

public class ClaimService : IClaimService
{
    private readonly AuthDbContext _context;

    public ClaimService(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<List<Claim>> ListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _context.AuthInfos
            .AsNoTracking()
            .SingleAsync(u => u.UserId == userId, cancellationToken);

        var claims = new List<Claim>();
        
        claims.Add(new Claim(ClaimTypeConstants.UserId, user.UserId.ToString()));
        claims.Add(new Claim(ClaimTypeConstants.Email, user.Email));
        claims.Add(new Claim(ClaimTypeConstants.Role, user.Role.ToString()));

        return claims;
    }
}