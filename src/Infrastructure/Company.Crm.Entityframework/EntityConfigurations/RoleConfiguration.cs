using Company.Crm.Domain.Entities.Usr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasMany(d => d.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "RolePermission",
                l => l.HasOne<Permission>().WithMany().HasForeignKey("PermissionId"),
                r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => { j.HasKey("RoleId", "PermissionId"); });
    }
}