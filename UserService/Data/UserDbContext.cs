using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply entity configurations from files in directory "./Data/EntityConfigurations".
        // See  more: https://docs.microsoft.com/en-us/ef/core/modeling/ .
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}