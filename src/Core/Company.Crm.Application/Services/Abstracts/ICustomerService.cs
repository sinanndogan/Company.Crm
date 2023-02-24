using Company.Crm.Application.Dtos;
using Company.Framework.Dtos;
using Company.Framework.Service;

namespace Company.Crm.Application.Services.Abstracts;

public interface ICustomerService : ICrudService<CustomerDto, CreateOrUpdateCustomerDto>
{
    ServiceResponse<List<CustomerDto>> GetAllByRegionId(int regionId);
}