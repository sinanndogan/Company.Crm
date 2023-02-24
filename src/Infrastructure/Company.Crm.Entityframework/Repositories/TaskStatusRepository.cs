using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Entityframework.Repositories;

public class TaskStatusRepository : BaseRepository<AppDbContext, TaskStatus>, ITaskStatusRepository
{
    public TaskStatusRepository(AppDbContext context) : base(context)
    {
    }
}
