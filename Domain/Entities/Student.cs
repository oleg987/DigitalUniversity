namespace Domain.Entities;

public class Student : User
{
    public ICollection<Subject> Subjects { get; private set; }

    public Student(Guid id, string name, string email) : base(id, name, email, UserRole.Student)
    {
        Subjects = new HashSet<Subject>();
    }

    /// <summary>
    /// For EF Core.
    /// </summary>
    protected Student() : base()
    {
        
    }
}