using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class DocumentTypeService : IDocumentTypeService
{
    private readonly IDocumentTypeRepository _documentTypeRepository;

    public DocumentTypeService(IDocumentTypeRepository documentTypeRepository)
    {
        _documentTypeRepository = documentTypeRepository;
    }

    public bool Delete(DocumentType entity)
    {
        return _documentTypeRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _documentTypeRepository.DeleteById(id);
    }

    public List<DocumentType> GetAll()
    {
        return _documentTypeRepository.GetAll().ToList();
    }

    public DocumentType? GetById(int id)
    {
        return _documentTypeRepository.GetById(id);
    }

    public bool Insert(DocumentType entity)
    {
        return _documentTypeRepository.Insert(entity);
    }

    public bool Update(DocumentType entity)
    {
        return _documentTypeRepository.Update(entity);
    }

    public List<DocumentType> GetPaged(int page = 1)
    {
        var entityList = _documentTypeRepository.GetAll();


        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();

        return pagedList;
    }
}