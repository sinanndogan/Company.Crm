using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class SettingRepository : BaseRepository<AppDbContext, Setting>, ISettingRepository
{
    public SettingRepository(AppDbContext ctx) : base(ctx)
    {
    }
}