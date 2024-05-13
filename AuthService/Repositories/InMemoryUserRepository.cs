using AuthService.Entities;

namespace AuthService.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = [];


    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? Get(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
}