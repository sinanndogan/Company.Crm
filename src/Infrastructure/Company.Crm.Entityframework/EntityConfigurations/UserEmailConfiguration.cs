using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class UserEmailConfiguration : IEntityTypeConfiguration<UserEmail>
{
    public void Configure(EntityTypeBuilder<UserEmail> builder)
    {
        builder.Property(d => d.UserId).IsRequired();
        builder.Property(d => d.EmailAddress).IsRequired();
        builder.Property(d => d.EmailType).IsRequired();
    }
}