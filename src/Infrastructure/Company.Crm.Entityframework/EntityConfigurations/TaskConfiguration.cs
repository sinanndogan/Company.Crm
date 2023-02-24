using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Company.Crm.Domain.Entities.Task;

namespace Company.Crm.Entityframework.EntityConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.Property(c => c.RequestId).IsRequired();
        builder.Property(c => c.EmployeeUserId).IsRequired();
        builder.Property(c => c.TaskStatusId).IsRequired();
        builder.Property(c => c.TaskStartDate).IsRequired();
        builder.Property(c => c.TaskEndDate).IsRequired();
        builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
    }
}