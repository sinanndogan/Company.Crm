using Company.Crm.Domain.Entities.Usr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.Password).HasMaxLength(200);
        builder.Property(c => c.RefreshToken).HasMaxLength(64);

        builder
            .HasMany(d => d.Roles)
            .WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "RoleId");
                    j.ToTable("UserRole");
                    j.HasIndex(new[] { "RoleId" }, "IX_UserRole_RoleId");
                });
    }
}