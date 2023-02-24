using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsRead { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}