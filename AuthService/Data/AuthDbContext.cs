using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }

    public DbSet<AuthInfo> AuthInfos { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply entity configurations from files in directory "./Data/EntityConfigurations".
        // See  more: https://docs.microsoft.com/en-us/ef/core/modeling/ .
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}