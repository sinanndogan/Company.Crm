using AutoMapper;
using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;
using Company.Framework.Utilities;

namespace Company.Crm.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IMapper _mapper;
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public ServiceResponse<List<NotificationDetailDto>> GetAll()
    {
        var entityList = _notificationRepository.GetAll();
        var dtoList = _mapper.Map<List<NotificationDetailDto>>(entityList);
        return new ServiceResponse<List<NotificationDetailDto>>(dtoList);
    }

    public ServicePaginationResponse<List<NotificationDetailDto>> GetPaged(PaginationRequest request)
    {
        var query = _notificationRepository.GetAll().OrderByDescending(x => x.IsRead);
        var totalCount = query.Count();
        var pagedList = query.ToPagedList(request);
        var dtoList = _mapper.Map<List<NotificationDetailDto>>(pagedList);
        return new ServicePaginationResponse<List<NotificationDetailDto>>(dtoList, totalCount, request);
    }

    public ServiceResponse<NotificationDetailDto> GetById(int id)
    {
        var entity = _notificationRepository.GetById(id);
        if (entity == null)
            return new ServiceResponse<NotificationDetailDto>("Not Found!");
        var dto = _mapper.Map<NotificationDetailDto>(entity);
        return new ServiceResponse<NotificationDetailDto>(dto);
    }

    public ServiceResponse<NotificationCreateOrUpdateDto> GetForEditById(int id)
    {
        var entity = _notificationRepository.GetById(id);
        var dto = _mapper.Map<NotificationCreateOrUpdateDto>(entity);
        return new ServiceResponse<NotificationCreateOrUpdateDto>(dto);
    }

    public ServiceResponse<bool> Insert(NotificationCreateOrUpdateDto dto)
    {
        var entity = _mapper.Map<Notification>(dto);
        return new ServiceResponse<bool>(_notificationRepository.Insert(entity));
    }

    public ServiceResponse<bool> MarkAsReadOrUnread(int id)
    {
        var notification = _notificationRepository.GetById(id);
        if (notification != null)
        {
            notification.IsRead = !notification.IsRead;
            return new ServiceResponse<bool>(_notificationRepository.Update(notification)); //save yapabilmek için /servislerden çağırmak için repositorye save methodu eklenebilir ? 
        }

        return new ServiceResponse<bool>(false);
    }

    public ServiceResponse<bool> Update(NotificationCreateOrUpdateDto dto)
    {
        var entity = _mapper.Map<Notification>(dto);
        return new ServiceResponse<bool>(_notificationRepository.Update(entity));
    }

    public ServiceResponse<bool> Delete(Notification dto)
    {
        var entity = _mapper.Map<Notification>(dto);
        return new ServiceResponse<bool>(_notificationRepository.Delete(entity));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_notificationRepository.DeleteById(id));
    }
}