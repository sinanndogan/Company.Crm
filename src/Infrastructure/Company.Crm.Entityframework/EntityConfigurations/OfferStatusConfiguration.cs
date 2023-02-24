using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class OfferStatusConfiguration : IEntityTypeConfiguration<OfferStatus>
{
    public void Configure(EntityTypeBuilder<OfferStatus> builder)
    {
        builder.ToTable(nameof(OfferStatus), "LST");
        builder.Property(o => o.Id).IsRequired();
        builder.Property(o => o.Name).HasMaxLength(50);
    }
}