using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Usr;

public class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}