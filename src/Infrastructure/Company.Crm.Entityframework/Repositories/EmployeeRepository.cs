using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class EmployeeRepository : BaseRepository<AppDbContext, Employee>, IEmployeeRepository
{
    private readonly AppDbContext _ctx;

    public EmployeeRepository(AppDbContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public List<Employee> GetAllByRegionId(int regionId)
    {
        return _ctx.Employees.Where(e => e.RegionId == regionId).ToList();
    }
}