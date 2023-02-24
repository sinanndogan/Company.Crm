using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.Notification;

public class NotificationCreateOrUpdateDto : BaseDto
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}