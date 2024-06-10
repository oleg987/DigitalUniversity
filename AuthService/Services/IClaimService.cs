using System.Security.Claims;

namespace AuthService.Services;

public interface IClaimService
{
    Task<List<Claim>> ListAsync(Guid userId);
}