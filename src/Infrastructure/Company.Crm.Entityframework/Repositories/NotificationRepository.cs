using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class NotificationRepository : BaseRepository<AppDbContext, Notification>, INotificationRepository
{
    public NotificationRepository(AppDbContext ctx) : base(ctx)
    {
    }
}