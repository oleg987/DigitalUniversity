using Domain.Entities;

namespace AuthService.Repositories;

public interface IUserRepository
{
    Task Add(User user, CancellationToken cancellationToken = default);
    Task<User?> Get(Guid id, CancellationToken cancellationToken = default);
}