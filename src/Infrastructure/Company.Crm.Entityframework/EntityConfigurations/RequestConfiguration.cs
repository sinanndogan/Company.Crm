using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Crm.Entityframework.EntityConfigurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.Property(c => c.EmployeeUserId).IsRequired();
            builder.Property(c => c.RequestStatusId).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.CustomerUserId).IsRequired();

        }

    }
}