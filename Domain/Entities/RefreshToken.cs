namespace Domain.Entities;

public class RefreshToken
{
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public long Expires { get; private set; }

    public RefreshToken(string token, Guid userId, long expires)
    {
        Token = token;
        UserId = userId;
        Expires = expires;
    }

    private RefreshToken()
    {
        
    }
}