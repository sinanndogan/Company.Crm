using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("DocumentType", Schema = "LST")]
public class DocumentType : BaseEntity
{
    public string? Name { get; set; }
}
