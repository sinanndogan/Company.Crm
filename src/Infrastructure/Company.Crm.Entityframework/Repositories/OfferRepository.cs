using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class OfferRepository : BaseRepository<AppDbContext, Offer>, IOfferRepository
{
    public OfferRepository(AppDbContext ctx) : base(ctx)
    {
    }
}