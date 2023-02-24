using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("Department", Schema = "LST")]
public class Department : BaseEntity
{
    public string? Name { get; set; }
}