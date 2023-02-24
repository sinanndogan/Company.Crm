using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class OfferStatusRepository : BaseRepository<AppDbContext, OfferStatus>, IOfferStatusRepository
{
    public OfferStatusRepository(AppDbContext ctx) : base(ctx)
    {
    }
}