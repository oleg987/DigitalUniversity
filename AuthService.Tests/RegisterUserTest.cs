using AuthService.Commands;
using AuthService.Entities;
using AuthService.Publisher;
using AuthService.Repositories;

namespace AuthService.Tests;

public class RegisterUserTest
{
    [Fact]
    public void RegisterUser_Success()
    {
        // Arrange
        var id = Guid.NewGuid(); // Guid.Parse("{static_value}")
        var name = "John Connor";
        var email = "j.connor@example.com";
        var role = UserRole.Student;

        var repository = new InMemoryUserRepository();

        var publisher = new FakeEventPublisher();
        
        var command = new RegisterUserCommand(id, email, name, role, repository, publisher);
        
        // Act
        command.Execute();
        
        var createdUser = repository.Get(id);

        var createdUserEvent = publisher.Messages
            .Single(m => m.Email == email);
        // Assert

        Assert.NotNull(createdUser);
        Assert.Equal(name, createdUser.Name);
        Assert.Equal(email, createdUser.Email);
        Assert.Equal(role, createdUser.Role);
        
        Assert.Equal(createdUser.InviteCode, createdUserEvent.InviteCode);
    }
}