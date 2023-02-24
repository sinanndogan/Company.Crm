using AutoMapper;
using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Application.Dtos.Request;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Company.Crm.Application.Services;

public class RequestService : IRequestService
{
    private readonly IMapper _mapper;
    private readonly IRequestRepository _requestRepository;

    public RequestService(IRequestRepository requestService, IMapper mapper)
    {
        _requestRepository = requestService;
        _mapper = mapper;
    }

    public List<RequestDto> GetAll()
    {
        var entityList = _requestRepository.GetAll();
        var dtoList = _mapper.Map<List<RequestDto>>(entityList);
        return dtoList;
    }

    public RequestDto? GetById(int id)
    {
        var entity = _requestRepository.GetById(id);
        return _mapper.Map<RequestDto>(entity);
    }

    public CreateOrUpdateRequestDto? GetForEditById(int id)
    {
        var entity = _requestRepository.GetById(id);
        var dto = _mapper.Map<CreateOrUpdateRequestDto>(entity);
        return dto;
    }

    public bool Insert(CreateOrUpdateRequestDto dto)
    {
        var entity = _mapper.Map<Request>(dto);
        return _requestRepository.Insert(entity);
    }

    public bool Update(CreateOrUpdateRequestDto dto)
    {
        var entity = _mapper.Map<Request>(dto);
        return _requestRepository.Update(entity);
    }

    public bool Delete(RequestDto dto)
    {
        var entity = _mapper.Map<Request>(dto);
        return _requestRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _requestRepository.DeleteById(id);
    }

    public List<RequestDto> GetPaged(int page = 1)
    {
        var entityList = _requestRepository.GetAll()
            .Include(e => e.RequestStatusId)
            .OrderByDescending(c => c.Id);

        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();

        var dtoList = _mapper.Map<List<RequestDto>>(pagedList);

        return dtoList;
    }
}