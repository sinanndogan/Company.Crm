using AutoMapper;
using Company.Crm.Application.Dtos;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Company.Crm.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public ServiceResponse<List<EmployeeDto>> GetAll()
    {
        var entityList = _employeeRepository.GetAll();
        var dtoList = _mapper.Map<List<EmployeeDto>>(entityList);

        return new ServiceResponse<List<EmployeeDto>>(dtoList);
    }

    public ServiceResponse<EmployeeDto?> GetById(int id)
    {
        var entity = _employeeRepository.GetById(id);
        var dto = _mapper.Map<EmployeeDto>(entity);
        return new ServiceResponse<EmployeeDto?>(dto);
    }

    public ServiceResponse<bool> Insert(CreateOrUpdateEmployeeDto dto)
    {
        var entity = _mapper.Map<Employee>(dto);
        return new ServiceResponse<bool>(_employeeRepository.Insert(entity));
    }

    public ServiceResponse<bool> Update(CreateOrUpdateEmployeeDto dto)
    {
        var entity = _mapper.Map<Employee>(dto);
        return new ServiceResponse<bool>(_employeeRepository.Update(entity));
    }

    public ServiceResponse<bool> Delete(EmployeeDto dto)
    {
        var entity = _mapper.Map<Employee>(dto);
        return new ServiceResponse<bool>(_employeeRepository.Delete(entity));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_employeeRepository.DeleteById(id));
    }

    public ServiceResponse<List<EmployeeDto>> GetAllByRegionId(int regionId)
    {
        var entityList = _employeeRepository.GetAllByRegionId(regionId);
        var dtoList = _mapper.Map<List<EmployeeDto>>(entityList);
        return new ServiceResponse<List<EmployeeDto>>(dtoList);
    }

    public ServiceResponse<EmployeeDto?> GetByUserId(int userId)
    {
        var entity = _employeeRepository.GetAll()
            .Include(e => e.UserFk)
            .Include(e => e.TitleFk)
            .FirstOrDefault(e => e.UserId == userId);
        var dto = _mapper.Map<EmployeeDto>(entity);
        return new ServiceResponse<EmployeeDto?>(dto);
    }
}