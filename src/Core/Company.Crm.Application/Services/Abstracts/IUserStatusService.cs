using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface IUserStatusService
{
    ServiceResponse<List<UserStatus>> GetAll();
    ServiceResponse<UserStatus?> GetById(int id);
    ServiceResponse<bool> Insert(UserStatus entity);
    ServiceResponse<bool> Update(UserStatus entity);
    ServiceResponse<bool> Delete(UserStatus entity);
    ServiceResponse<bool> DeleteById(int id);
    ServicePaginationResponse<List<UserStatus>> GetPaged(PaginationRequest request);

    ServiceResponse<UserStatus> GetForEditById(int id);
}