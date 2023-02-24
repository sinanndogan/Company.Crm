using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class RegionRepository : BaseRepository<AppDbContext, Region>, IRegionRepository
{
    public RegionRepository(AppDbContext ctx) : base(ctx)
    {
    }
}