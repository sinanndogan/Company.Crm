using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("RequestStatus", Schema = "LST")]
public class RequestStatus : BaseEntity
{
    public string Name { get; set; }
}
