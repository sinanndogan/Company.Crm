using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.ToTable(nameof(Gender), "LST");
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Name).HasMaxLength(50);
    }
}