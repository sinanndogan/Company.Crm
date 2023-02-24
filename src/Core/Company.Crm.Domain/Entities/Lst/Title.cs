using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("Title", Schema = "LST")]
public class Title : BaseEntity
{
    public string Name { get; set; }
}