namespace Domain.Entities;

public class Subject
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public Professor Professor { get; private set; }
    public ICollection<Student> Students { get; private set; }

    public Subject(Guid id, string title, Professor professor)
    {
        Id = id;
        Title = title;
        Professor = professor;
        Students = new HashSet<Student>();
    }

    private Subject()
    {
        
    }
}