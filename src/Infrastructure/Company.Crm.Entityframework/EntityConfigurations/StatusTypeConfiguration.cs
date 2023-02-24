using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class StatusTypeConfiguration : IEntityTypeConfiguration<StatusType>
{
    public void Configure(EntityTypeBuilder<StatusType> builder)
    {
        builder.ToTable(nameof(StatusType), "LST");
        builder.Property(st => st.Id).IsRequired();
        builder.Property(st => st.Name).IsRequired();
        builder.Property(st => st.Name).HasMaxLength(30);
    }
}