using Company.Framework.Repository;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Domain.Repositories;

public interface ITaskStatusRepository : IRepository<TaskStatus>
{
}
