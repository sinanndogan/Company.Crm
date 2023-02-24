using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Domain.Entities;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface INotificationService
{
    ServicePaginationResponse<List<NotificationDetailDto>> GetPaged(PaginationRequest request);
    ServiceResponse<List<NotificationDetailDto>> GetAll();
    ServiceResponse<NotificationDetailDto> GetById(int id);
    ServiceResponse<NotificationCreateOrUpdateDto> GetForEditById(int id);
    ServiceResponse<bool> Insert(NotificationCreateOrUpdateDto dto);
    ServiceResponse<bool> Update(NotificationCreateOrUpdateDto dto);
    ServiceResponse<bool> Delete(Notification entity);
    ServiceResponse<bool> DeleteById(int id);
    ServiceResponse<bool> MarkAsReadOrUnread(int id);
}