using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Usr;

public class Permission : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Role> Roles { get; set; }
}