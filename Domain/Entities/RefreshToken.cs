using System.Security.Cryptography;

namespace Domain.Entities;

public class RefreshToken
{
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public long Expires { get; private set; }

    private RefreshToken(string token, Guid userId, long expires)
    {
        Token = token;
        UserId = userId;
        Expires = expires;
    }

    private RefreshToken()
    {
        
    }

    public static RefreshToken Create(Guid userId, TimeSpan lifetime)
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        
        var token = Convert.ToBase64String(bytes);
        
        var refreshTokenExpires = DateTimeOffset.UtcNow.Add(lifetime).ToUnixTimeSeconds();

        return new RefreshToken(token, userId, refreshTokenExpires);
    }

    public bool IsNotExpired()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds() <= Expires;
    }
}