using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class Setting : BaseEntity
{
    public int UserId { get; set; }
    public string SettingKey { get; set; }
    public string SettingValue { get; set; }
}