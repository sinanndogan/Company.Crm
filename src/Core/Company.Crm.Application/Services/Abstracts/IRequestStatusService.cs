using Company.Crm.Domain.Entities.Lst;

namespace Company.Crm.Application.Services.Abstracts;

public interface IRequestStatusService
{
    List<RequestStatus> GetAll();
    RequestStatus? GetById(int id);
    bool Insert(RequestStatus entity);
    bool Update(RequestStatus entity);
    bool Delete(RequestStatus entity);
    bool DeleteById(int id);
    List<RequestStatus> GetPaged(int page = 1);
    RequestStatus? GetForEditById(int id);
}