using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("Region", Schema = "LST")]
public class Region : BaseEntity
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}