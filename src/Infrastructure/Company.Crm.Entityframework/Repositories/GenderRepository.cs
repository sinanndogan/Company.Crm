using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories
{
    public class GenderRepository : BaseRepository<AppDbContext, Gender>, IGenderRepository
    {
        private readonly AppDbContext _ctx;

        public GenderRepository(AppDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}