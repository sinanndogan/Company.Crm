using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface IRegionService
{
    ServiceResponse<List<Region>> GetAll();
    ServiceResponse<Region?> GetById(int id);
    ServiceResponse<bool> Insert(Region entity);
    ServiceResponse<bool> Update(Region entity);
    ServiceResponse<bool> Delete(Region entity);
    ServiceResponse<bool> DeleteById(int id);
    ServicePaginationResponse<List<Region>> GetPaged(PaginationRequest request);
    ServiceResponse<Region> GetForEditById(int id);


   
}