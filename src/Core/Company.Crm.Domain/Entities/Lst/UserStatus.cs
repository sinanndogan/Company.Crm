using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Lst;

[Table("UserStatus", Schema = "LST")]
public class UserStatus : BaseEntity
{
    public string Name { get; set; }
}