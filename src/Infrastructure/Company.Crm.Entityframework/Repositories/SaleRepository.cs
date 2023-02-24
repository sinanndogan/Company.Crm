using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories
{
    public class SaleRepository : BaseRepository<AppDbContext, Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
