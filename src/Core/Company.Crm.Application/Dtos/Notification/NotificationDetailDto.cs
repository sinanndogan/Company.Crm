using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.Notification;

public class NotificationDetailDto : BaseDto
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public bool IsRead { get; set; }
}