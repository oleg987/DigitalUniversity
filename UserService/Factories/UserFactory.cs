using Domain.Entities;
using UserService.Requests;

namespace UserService.Factories;

public class UserFactory
{
    public User Create(CreateUserRequest request)
    {
        return request.Role switch
        {
            UserRole.Student => new Student(request.Id, request.Name, request.Email),
            UserRole.Professor => new Professor(request.Id, request.Name, request.Email),
            UserRole.StudyDepartment => new StudyDepartment(request.Id, request.Name, request.Email),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}