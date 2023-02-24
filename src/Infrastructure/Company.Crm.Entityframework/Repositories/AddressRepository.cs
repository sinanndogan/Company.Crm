using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class UserAddressRepository : BaseRepository<AppDbContext, UserAddress>, IUserAddressRepository
{
    public UserAddressRepository(AppDbContext ctx) : base(ctx)
    {
    }
}