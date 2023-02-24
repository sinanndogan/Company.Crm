using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.BirthDate).HasColumnType("date").IsRequired();
        builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(500);
    }
}