using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Repository;

namespace Company.Crm.Entityframework.Repositories;

public class DocumentTypeRepository : BaseRepository<AppDbContext, DocumentType>, IDocumentTypeRepository
{
    public DocumentTypeRepository(AppDbContext context) : base(context)
    {
    }
}
