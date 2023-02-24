using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
{
    public void Configure(EntityTypeBuilder<UserStatus> builder)
    {
        builder.Property(z => z.Name).IsRequired().HasMaxLength(50);
    }
}