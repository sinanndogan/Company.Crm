using Company.Crm.Application.Dtos.Address;
using Company.Crm.Application.Dtos.UserAddress;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface IUserAddressService
{
    ServiceResponse<List<AddressDetailDto>> GetAll();
    ServicePaginationResponse<List<AddressDetailDto>> GetPaged(PaginationRequest request);

    ServiceResponse<AddressDetailDto?> GetById(int id);

    ServiceResponse<bool> Insert(AddressCreateOrUpdateDto dto);
    ServiceResponse<bool> Update(AddressCreateOrUpdateDto dto);
    ServiceResponse<bool> Delete(AddressDetailDto dto);
    ServiceResponse<bool> DeleteById(int id);
    ServiceResponse<AddressCreateOrUpdateDto?> GetForEditById(int id);
}