using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class UserPhoneRepository : BaseRepository<AppDbContext, UserPhone>, IUserPhoneRepository
{
    public UserPhoneRepository(AppDbContext ctx) : base(ctx)
    {
    }
}