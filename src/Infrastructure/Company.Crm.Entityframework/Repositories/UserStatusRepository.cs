using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class UserStatusRepository : BaseRepository<AppDbContext, UserStatus>, IUserStatusRepository
{
    public UserStatusRepository(AppDbContext ctx) : base(ctx)
    {
    }
}