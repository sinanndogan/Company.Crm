using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.Property(s => s.RequestId).IsRequired();
        builder.Property(s => s.EmployeeUserId).IsRequired();
        builder.Property(s => s.SaleDate).IsRequired();
        builder.Property(s => s.SaleAmount).IsRequired();
        builder.Property(s => s.Description).IsRequired();
        builder.Property(e => e.SaleAmount).HasPrecision(12, 2);
    }
}