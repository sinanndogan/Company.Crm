using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.RequestId).IsRequired();
        builder.Property(a => a.DocumentTypeId).IsRequired();
        builder.Property(a => a.DocumentFileName).IsRequired();
    }
}