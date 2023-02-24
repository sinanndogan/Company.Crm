using Company.Crm.Domain.Enums;
using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class UserEmail : BaseEntity
{
    public int? UserId { get; set; }
    public string? EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }
}