using AuthService.Entities;

namespace AuthService.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? Get(Guid id);
}