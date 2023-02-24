using Company.Framework.Repository;
using Task = Company.Crm.Domain.Entities.Task;

namespace Company.Crm.Domain.Repositories;

public interface ITaskRepository : IRepository<Task>
{
}