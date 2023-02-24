using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;
using Task = Company.Crm.Domain.Entities.Task;

namespace Company.Crm.Entityframework.Repositories;

public class TaskRepository : BaseRepository<AppDbContext, Task>, ITaskRepository
{
    public TaskRepository(AppDbContext ctx) : base(ctx)
    {
    }
}