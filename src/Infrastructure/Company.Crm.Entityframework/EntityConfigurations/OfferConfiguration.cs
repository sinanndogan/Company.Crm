using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.Property(t => t.RequestId).IsRequired();
        builder.Property(t => t.EmployeeUserId).IsRequired();
        builder.Property(t => t.OfferStatusId).IsRequired();
        builder.Property(t => t.BidAmount).HasPrecision(12, 2).IsRequired();
    }
}