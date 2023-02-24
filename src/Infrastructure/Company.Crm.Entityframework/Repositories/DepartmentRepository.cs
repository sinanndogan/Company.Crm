using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class DepartmentRepository : BaseRepository<AppDbContext, Department>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext ctx) : base(ctx)
    {
    }
}