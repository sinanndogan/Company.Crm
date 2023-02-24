using Company.Crm.Domain.Enums;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class UserAddress : BaseEntity
{
    public int UserId { get; set; }
    public string Description { get; set; }
    public AddressTypeEnum AddressType { get; set; }
}