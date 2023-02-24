using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class CustomerRepository : BaseRepository<AppDbContext, Customer>, ICustomerRepository
{
    private readonly AppDbContext _ctx;

    public CustomerRepository(AppDbContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public IEnumerable<Customer> GetAllByRegionId(int regionId)
    {
        var query = _ctx.Customers.Where(e => e.RegionId == regionId);

        return query;
    }
}