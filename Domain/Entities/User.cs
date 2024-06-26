﻿namespace Domain.Entities;

public enum UserRole
{
    Student = 1,
    Professor = 2,
    StudyDepartment = 3
}

public abstract class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    /// <summary>
    /// User e-mail. Unique.
    /// </summary>
    public string Email { get; private set; }
    public UserRole Role { get; private set; }

    protected User(Guid id, string name, string email, UserRole role)
    {
        Id = id;
        ValidateName(name);
        Name = name;
        ValidateEmail(email);
        Email = email;
        Role = role;
    }

    /// <summary>
    /// For EF Core.
    /// </summary>
    protected User()
    {
        
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

public class StudyDepartment : User
{
    public StudyDepartment(Guid id, string name, string email) : base(id, name, email, UserRole.StudyDepartment)
    {
    }

    protected StudyDepartment() : base()
    {
        
    }
}