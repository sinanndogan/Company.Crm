using Company.Crm.Domain.Entities.Lst;

namespace Company.Crm.Application.Services.Abstracts;

public interface IStatusTypeService
{
    List<StatusType> GetAll();
    StatusType? GetById(int id);
    bool Insert(StatusType entity);
    bool Update(StatusType entity);
    bool Delete(StatusType entity);
    bool DeleteById(int id);
}