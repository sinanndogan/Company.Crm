using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class RequestRepository : BaseRepository<AppDbContext, Request>, IRequestRepository
{
    public RequestRepository(AppDbContext ctx) : base(ctx)
    {
    }
}