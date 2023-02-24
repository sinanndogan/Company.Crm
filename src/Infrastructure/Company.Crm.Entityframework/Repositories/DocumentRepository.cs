using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class DocumentRepository : BaseRepository<AppDbContext, Document>, IDocumentRepository
{
    public DocumentRepository(AppDbContext ctx) : base(ctx)
    {
    }
}