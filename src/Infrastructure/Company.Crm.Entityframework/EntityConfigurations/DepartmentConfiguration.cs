using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(nameof(Department), "LST");
        builder.Property(o => o.Id).IsRequired();
        builder.Property(o => o.Name).HasMaxLength(50);
    }
}