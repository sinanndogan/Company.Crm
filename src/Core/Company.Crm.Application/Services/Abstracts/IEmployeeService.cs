using Company.Crm.Application.Dtos;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface IEmployeeService
{
    ServiceResponse<List<EmployeeDto>> GetAll();
    ServiceResponse<EmployeeDto?> GetById(int id);
    ServiceResponse<bool> Insert(CreateOrUpdateEmployeeDto dto);
    ServiceResponse<bool> Update(CreateOrUpdateEmployeeDto dto);
    ServiceResponse<bool> Delete(EmployeeDto dto);
    ServiceResponse<bool> DeleteById(int id);
    ServiceResponse<List<EmployeeDto>> GetAllByRegionId(int regionId);
    ServiceResponse<EmployeeDto?> GetByUserId(int userId);
}