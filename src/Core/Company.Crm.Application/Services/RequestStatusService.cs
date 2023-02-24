using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class RequestStatusService : IRequestStatusService
{
    private readonly IRequestStatusRepository _requestStatusRepository;

    public RequestStatusService(IRequestStatusRepository requestStatusRepository)
    {
        _requestStatusRepository = requestStatusRepository;
    }

    public List<RequestStatus> GetAll()
    {
        return _requestStatusRepository.GetAll().ToList();
    }

    public List<RequestStatus> GetPaged(int page = 1)
    {
        var entityList = _requestStatusRepository.GetAll().OrderByDescending(c => c.Id);
        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();
        return pagedList;
    }

    public RequestStatus? GetById(int id)
    {
        return _requestStatusRepository.GetById(id);
    }

    public RequestStatus? GetForEditById(int id)
    {
        var requeststatus = _requestStatusRepository.GetById(id);
        return requeststatus;
    }

    public bool Insert(RequestStatus entity)
    {
        return _requestStatusRepository.Insert(entity);
    }

    public bool Update(RequestStatus entity)
    {
        return _requestStatusRepository.Update(entity);
    }

    public bool Delete(RequestStatus entity)
    {
        return _requestStatusRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _requestStatusRepository.DeleteById(id);
    }
}