using AuthService.Commands;
using AuthService.Publisher;
using AuthService.Repositories;
using AuthService.Requests;
using Domain.Entities;

namespace AuthService.Tests;

public class CreateUserTest
{
    [Fact]
    public async Task CreateUser_Success()
    {
        // Arrange
        var name = "John Connor";
        var email = "j.connor@example.com";
        var role = UserRole.Student;

        var request = new CreateUserRequest(name, email, role);

        var repository = new InMemoryUserRepository();

        var publisher = new FakeEventPublisher();
        
        var command = new CreateUserCommand(request, repository, publisher);
        
        // Act
        await command.Execute();
        
        var createdUser = await repository.Get(request.Id);

        var createdUserEvent = publisher.Messages
            .Single(m => m.Email == email);
        // Assert

        Assert.NotNull(createdUser);
        Assert.Equal(name, createdUser.Name);
        Assert.Equal(email, createdUser.Email);
        Assert.Equal(role, createdUser.Role);
        
        Assert.Equal(createdUser.InviteCode, createdUserEvent.InviteCode);
    }
    
    [Fact]
    public async Task CreateUser_InvalidEmail()
    {
        var name = "John Connor";
        var email = "j-connor.example.com";
        var role = UserRole.Student;

        var request = new CreateUserRequest(name, email, role);

        var repository = new InMemoryUserRepository();

        var publisher = new FakeEventPublisher();
        
        var command = new CreateUserCommand(request, repository, publisher);

        await Assert.ThrowsAsync<ArgumentException>(async () => await command.Execute());
        
        var createdUser = await repository.Get(request.Id);

        var createdUserEvent = publisher.Messages
            .SingleOrDefault(m => m.Email == email);
        
        Assert.Null(createdUser);
        Assert.Null(createdUserEvent);
    }
    
    [Fact]
    public async Task CreateUser_InvalidName()
    {
        var name = "J";
        var email = "j-connor@example.com";
        var role = UserRole.Student;

        var request = new CreateUserRequest(name, email, role);

        var repository = new InMemoryUserRepository();

        var publisher = new FakeEventPublisher();
        
        var command = new CreateUserCommand(request, repository, publisher);

        await Assert.ThrowsAsync<ArgumentException>(async () => await command.Execute());
        
        var createdUser = await repository.Get(request.Id);

        var createdUserEvent = publisher.Messages
            .SingleOrDefault(m => m.Email == email);
        
        Assert.Null(createdUser);
        Assert.Null(createdUserEvent);
    }
}