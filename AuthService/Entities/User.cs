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
        Name = name;
        Email = email;
        Role = role;
        InviteCode = GenerateInvite();
    }

    private string GenerateInvite()
    {
        return Guid.NewGuid().ToString();
    }
}