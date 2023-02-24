using Company.Crm.Domain.Entities.Lst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class TaskStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.Lst.TaskStatus>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Lst.TaskStatus> builder)
    {
        builder.ToTable(nameof(Domain.Entities.Lst.TaskStatus), "LST");
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
       
    }
}