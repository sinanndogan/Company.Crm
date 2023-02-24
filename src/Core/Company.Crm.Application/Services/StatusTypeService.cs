using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Company.Crm.Application.Services;

public class StatusTypeService : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository;

    public StatusTypeService(IStatusTypeRepository statusTypeRepository)
    {
        _statusTypeRepository = statusTypeRepository;
    }

    public List<StatusType> GetAll()
    {
        var statusTypes = _statusTypeRepository.GetAll()
            .Include(e => e.Customers);

        return statusTypes.ToList();
    }

    public StatusType? GetById(int id)
    {
        return _statusTypeRepository.GetById(id);
    }

    public bool Insert(StatusType entity)
    {
        return _statusTypeRepository.Insert(entity);
    }

    public bool Update(StatusType entity)
    {
        return _statusTypeRepository.Update(entity);
    }

    public bool Delete(StatusType entity)
    {
        return _statusTypeRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _statusTypeRepository.DeleteById(id);
    }
}