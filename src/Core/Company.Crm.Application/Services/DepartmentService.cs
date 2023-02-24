using AutoMapper;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public ServiceResponse<List<Department>> GetAll()
    {
        var entityList = _departmentRepository.GetAll().ToList();
        return new ServiceResponse<List<Department>>(entityList);
    }

    public ServicePaginationResponse<List<Department>> GetPaged(PaginationRequest req)
    {
        var entityQuery = _departmentRepository.GetAll().OrderByDescending(c => c.Id);
        var totalEntity = entityQuery.Count();
        var pagedList = entityQuery.Skip(req.Skip).Take(req.PerPage).ToList();
        return new ServicePaginationResponse<List<Department>>(pagedList, totalEntity, req);
    }

    public ServiceResponse<Department?> GetById(int id)
    {
        var entity = _departmentRepository.GetById(id);
        return new ServiceResponse<Department>(entity);
    }

    public ServiceResponse<Department?> GetForEditById(int id)
    {
        var department = _departmentRepository.GetById(id);
        return new ServiceResponse<Department?>(department);
    }

    public ServiceResponse<bool> Insert(Department entity)
    {
        return new ServiceResponse<bool>(_departmentRepository.Insert(entity));
    }

    public ServiceResponse<bool> Update(Department entity)
    {
        return new ServiceResponse<bool>(_departmentRepository.Update(entity));
    }

    public ServiceResponse<bool> Delete(Department entity)
    {
        return new ServiceResponse<bool>(_departmentRepository.Delete(entity));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_departmentRepository.DeleteById(id));
    }
}