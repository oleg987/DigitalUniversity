using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Common.Settings;

public class JwtCredentials
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Secret { get; init; }
    public SymmetricSecurityKey SecurityKey => new(Encoding.UTF8.GetBytes(Secret));
}