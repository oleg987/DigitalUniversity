
using Domain.Entities;

namespace AuthService.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = [];
    public Task Add(User user, CancellationToken cancellationToken = default)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }
}