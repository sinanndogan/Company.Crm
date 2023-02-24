using AutoMapper;
using Company.Crm.Application.Dtos.Task;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Repositories;
using Company.Crm.Entityframework.Repositories;
using Company.Framework.Dtos;
using Microsoft.EntityFrameworkCore;
using Task = Company.Crm.Domain.Entities.Task;

namespace Company.Crm.Application.Services;

public class TaskService : ITaskService
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public ServiceResponse<List<TaskDto>> GetAll()
    {
        var entityList = _taskRepository.GetAll();

        var dtoList = _mapper.Map<List<TaskDto>>(entityList);

        return new ServiceResponse<List<TaskDto>>(dtoList);
    }


    public ServicePaginationResponse<List<TaskDto>> GetPaged(PaginationRequest req)
    {
        var entityQuery = _taskRepository.GetAll()
            //.Include(e => e.RequestFK)
            .Include(e => e.TaskStatusFK)
            //.Include(e => e.EmployeeFK)
            .OrderByDescending(c => c.Id);

        var totalEntity = entityQuery.Count();

        var pagedList = entityQuery.Skip(req.Skip).Take(req.PerPage).ToList();

        var dtoList = _mapper.Map<List<TaskDto>>(pagedList);

        return new ServicePaginationResponse<List<TaskDto>>(dtoList, totalEntity, req);
    }

    public ServiceResponse<TaskDto?> GetById(int id)
    {
        var entity = _taskRepository.GetById(id);
        var dto = _mapper.Map<TaskDto>(entity);
        return new ServiceResponse<TaskDto?>(dto);
    }

    public ServiceResponse<CreateOrUpdateTaskDto?> GetForEditById(int id)
    {
        var entity = _taskRepository.GetById(id);
        var dto = _mapper.Map<CreateOrUpdateTaskDto>(entity);
        return new ServiceResponse<CreateOrUpdateTaskDto?>(dto);
    }

    public ServiceResponse<bool> Insert(CreateOrUpdateTaskDto dto)
    {
        var task = _mapper.Map<Task>(dto);

        return new ServiceResponse<bool>(_taskRepository.Insert(task));
    }

    public ServiceResponse<bool> Update(CreateOrUpdateTaskDto dto)
    {
        var task = _mapper.Map<Task>(dto);

        return new ServiceResponse<bool>(_taskRepository.Update(task));
    }

    public ServiceResponse<bool> Delete(TaskDto dto)
    {
        var task = _mapper.Map<Task>(dto);

        return new ServiceResponse<bool>(_taskRepository.Delete(task));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_taskRepository.DeleteById(id));
    }
}