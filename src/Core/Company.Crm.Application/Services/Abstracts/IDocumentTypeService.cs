using Company.Crm.Domain.Entities.Lst;

namespace Company.Crm.Application.Services.Abstracts;

public interface IDocumentTypeService
{
    List<DocumentType> GetAll();
    DocumentType? GetById(int id);
    bool Insert(DocumentType entity);
    bool Update(DocumentType entity);
    bool Delete(DocumentType entity);
    bool DeleteById(int id);
    List<DocumentType> GetPaged(int page = 1);
}