using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class TitleConfiguration : IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.ToTable(nameof(Department), "LST");
        builder.Property(o => o.Id).IsRequired();
        builder.Property(o => o.Name).HasMaxLength(100);
    }
}