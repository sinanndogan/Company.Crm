using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Application.Dtos.Request;

namespace Company.Crm.Application.Services.Abstracts;

public interface IRequestService
{
    List<RequestDto> GetAll();
    CreateOrUpdateRequestDto? GetForEditById(int id);
    RequestDto? GetById(int id);
    bool Insert(CreateOrUpdateRequestDto entity);
    bool Update(CreateOrUpdateRequestDto entity);
    bool Delete(RequestDto entity);
    bool DeleteById(int id);
    List<RequestDto> GetPaged(int page = 1);
}