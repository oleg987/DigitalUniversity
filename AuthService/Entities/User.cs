namespace AuthService.Entities;

public enum UserRole
{
    Student = 1,
    Professor = 2,
    StudyDepartment = 3
}

public class User
{
    public Guid Id { get; }
    public string Name { get; }
    public string Email { get; }
    public UserRole Role { get; }
    public string InviteCode { get; }

    public User(Guid id, string name, string email, UserRole role)
    {
        Id = id;
        ValidateName(name);
        Name = name;
        ValidateEmail(email);
        Email = email;
        Role = role;
        InviteCode = GenerateInvite();
    }

    private string GenerateInvite()
    {
        return Guid.NewGuid().ToString();
    }

    private static void ValidateEmail(string email)
    {
        // TODO: Do more complex validation.
        
        if (!email.Contains('@'))
        {
            throw new ArgumentException("Invalid e-mail. Email must contain '@' character.");
        }
    }

    private static void ValidateName(string name)
    {
        // TODO: Do more complex validation.

        if (name.Length < 3)
        {
            throw new ArgumentException("Invalid name. Name too short.");
        }
    }
}