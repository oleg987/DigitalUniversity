using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AuthService.Data;
using AuthService.Requests;
using Common.Auth;
using Common.Settings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class JwtAuthService
{
    private readonly IClaimService _claimService;
    private readonly JwtCredentials _credentials;
    private readonly AuthDbContext _context;

    public JwtAuthService(IClaimService claimService, JwtCredentials credentials, AuthDbContext context)
    {
        _claimService = claimService;
        _credentials = credentials;
        _context = context;
    }

    public async Task<JwtTokenResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.AuthInfos.SingleAsync(u => u.Email == request.Email.ToLower(), cancellationToken);

        if (!user.CheckPassword(request.Password))
        {
            throw new Exception("Invalid password.");
        }

        var accessToken = await GenerateAccessToken(user, cancellationToken);

        var refreshToken = GenerateRefreshToken();
        
        var refreshTokenExpires = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(7)).ToUnixTimeSeconds();

        return new JwtTokenResponse(accessToken, refreshToken, refreshTokenExpires);
    }

    private async Task<string> GenerateAccessToken(AuthInfo user, CancellationToken cancellationToken)
    {
        var claims = await _claimService.ListAsync(user.UserId, cancellationToken);
        
        var accessTokenExpires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(5));
        
        var signingCredentials = new SigningCredentials(_credentials.SecurityKey, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _credentials.Issuer,
            audience: _credentials.Audience,
            claims: claims,
            expires: accessTokenExpires,
            signingCredentials: signingCredentials);
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token)!;

        return accessToken;
    }
    
    private static string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}