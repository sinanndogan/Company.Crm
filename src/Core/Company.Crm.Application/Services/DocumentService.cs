using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public List<Document> GetAll()
    {
        return _documentRepository.GetAll().ToList();
    }

    public Document? GetById(int id)
    {
        return _documentRepository.GetById(id);
    }

    public bool Insert(Document entity)
    {
        return _documentRepository.Insert(entity);
    }

    public bool Update(Document entity)
    {
        return _documentRepository.Update(entity);
    }

    public bool Delete(Document entity)
    {
        return _documentRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _documentRepository.DeleteById(id);
    }
}