using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserService.Data.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Forbid Id generation in database.
        builder.Property(e => e.Id)
            .ValueGeneratedNever();
        
        builder.HasIndex(e => e.Email)
            .IsUnique();
    }
}