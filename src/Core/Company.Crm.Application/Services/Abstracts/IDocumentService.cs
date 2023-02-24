using Company.Crm.Domain.Entities;

namespace Company.Crm.Application.Services.Abstracts;

public interface IDocumentService
{
    List<Document> GetAll();
    Document? GetById(int id);
    bool Insert(Document entity);
    bool Update(Document entity);
    bool Delete(Document entity);
    bool DeleteById(int id);
}