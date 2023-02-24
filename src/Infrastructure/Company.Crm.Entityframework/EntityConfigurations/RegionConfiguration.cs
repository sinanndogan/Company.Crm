using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable(nameof(Region), "LST");
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.ParentId).IsRequired(false);
    }
}