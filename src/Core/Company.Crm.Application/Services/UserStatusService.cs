using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services;

public class UserStatusService : IUserStatusService
{
    private readonly IUserStatusRepository _userStatusesRepository;

    public UserStatusService(IUserStatusRepository userStatusesRepository)
    {
        _userStatusesRepository = userStatusesRepository;
    }

    public ServiceResponse<List<UserStatus>> GetAll()
    {
        var list = _userStatusesRepository.GetAll().ToList();
        return new ServiceResponse<List<UserStatus>>(list);
    }

    public ServiceResponse<UserStatus?> GetById(int id)
    {
        var data = _userStatusesRepository.GetById(id);
        if (data == null) return new ServiceResponse<UserStatus?>("Not Found!");
        return new(data);
    }

    public ServiceResponse<UserStatus> GetForEditById(int id)
    {
        var data = _userStatusesRepository.GetById(id);
        return new(data);
    }

    public ServicePaginationResponse<List<UserStatus>> GetPaged(PaginationRequest request)
    {

        var query = _userStatusesRepository.GetAll();
        var total = query.Count();
        var pagedList = query.Skip(request.Skip).Take(request.PerPage).ToList();
        return new ServicePaginationResponse<List<UserStatus>>(pagedList, total, request);
    }

    public ServiceResponse<bool> Insert(UserStatus entity)
    {
        return new(_userStatusesRepository.Insert(entity));
    }

    public ServiceResponse<bool> Update(UserStatus entity)
    {
        return new(_userStatusesRepository.Update(entity));
    }


    public ServiceResponse<bool> Delete(UserStatus entity)
    {
        return new ServiceResponse<bool>(_userStatusesRepository.Delete(entity));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_userStatusesRepository.DeleteById(id));
    }
}