using System.Security.Cryptography;
using System.Text;

namespace Domain.Entities;

public class AuthInfo
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string InviteCode { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActivated { get; private set; }
    public string? PasswordHash { get; private set; }

    public AuthInfo(Guid userId, string email, int role)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Email = email;
        InviteCode = GenerateInviteCode();
        Role = (UserRole)role;
        IsActivated = false;
    }

    /// <summary>
    /// For EF Core.
    /// </summary>
    private AuthInfo()
    {
        
    }

    /// <summary>
    /// Generate InviteCode for User. Get random bytes and convert them into base64 string.
    /// </summary>
    /// <returns></returns>
    private string GenerateInviteCode()
    {
        var bytes = new byte[128];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    public void Activate(string invite, string password)
    {
        if (IsActivated)
        {
            throw new Exception("User is already activated.");
        }

        if (InviteCode != invite)
        {
            throw new Exception("Invalid invite code.");
        }

        if (password.Length < 6)
        {
            throw new Exception("Password too short.");
        }

        IsActivated = true;

        PasswordHash = HashFunction(password);
    }

    /// <summary>
    /// Function hash password string using SHA256 algorithm and convert hash into hexadecimal string.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private string HashFunction(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);

        using var algorithm = SHA256.Create();
        var hashBytes = algorithm.ComputeHash(bytes);

        var builder = new StringBuilder();

        foreach (var t in hashBytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }
}