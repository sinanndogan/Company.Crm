using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("StatusType", Schema = "LST")]
public class StatusType : BaseEntity
{
    public string Name { get; set; }

    public List<Customer>? Customers { get; set; }
}