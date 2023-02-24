using Company.Crm.Domain.Enums;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class UserPhone : BaseEntity
{
    public int UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public PhoneTypeEnum PhoneType { get; set; }
}