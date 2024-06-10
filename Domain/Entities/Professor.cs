namespace Domain.Entities;

public class Professor : User
{
    public ICollection<Subject> Subjects { get; private set; }

    public Professor(Guid id, string name, string email) : base(id, name, email, UserRole.Professor)
    {
        Subjects = new HashSet<Subject>();
    }

    protected Professor() : base()
    {
        
    }
}