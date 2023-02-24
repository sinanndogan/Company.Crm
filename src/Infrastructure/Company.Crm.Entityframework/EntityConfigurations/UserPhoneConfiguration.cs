using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
{
    public void Configure(EntityTypeBuilder<UserPhone> builder)
    {
        builder.Property(d => d.UserId).IsRequired();
        builder.Property(d => d.PhoneNumber).IsRequired();
        builder.Property(d => d.PhoneType).IsRequired();
    }
}