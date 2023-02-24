using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities.Usr;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; }

    public ICollection<Permission> Permissions { get; set; }
}