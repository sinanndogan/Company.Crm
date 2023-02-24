using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("TaskStatus", Schema = "LST")]
public class TaskStatus : BaseEntity
{
    public string? Name { get; set; }

    public List<Task>? Tasks { get; set; }
}
