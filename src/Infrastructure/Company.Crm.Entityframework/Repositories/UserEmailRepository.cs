using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class UserEmailRepository : BaseRepository<AppDbContext, UserEmail>, IUserEmailRepository
{
    public UserEmailRepository(AppDbContext ctx) : base(ctx)
    {
    }
}