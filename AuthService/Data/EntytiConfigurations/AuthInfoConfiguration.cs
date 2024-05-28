using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Data.EntytiConfigurations;

public class AuthInfoConfiguration : IEntityTypeConfiguration<AuthInfo>
{
    public void Configure(EntityTypeBuilder<AuthInfo> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.HasIndex(e => e.InviteCode)
            .IsUnique();

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasIndex(e => e.UserId)
            .IsUnique();
    }
}