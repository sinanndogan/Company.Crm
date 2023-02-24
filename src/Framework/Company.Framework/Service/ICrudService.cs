using Company.Framework.Dtos;

namespace Company.Framework.Service;

public interface ICrudService<TDto, TEditDto>
    where TDto : class
    where TEditDto : class
{
    ServiceResponse<List<TDto>> GetAll();
    ServicePaginationResponse<List<TDto>> GetPaged(PaginationRequest req);
    ServiceResponse<TDto?> GetById(int id);
    ServiceResponse<TEditDto?> GetForEditById(int id);
    ServiceResponse<bool> Insert(TEditDto entity);
    ServiceResponse<bool> Update(TEditDto entity);
    ServiceResponse<bool> Delete(TDto entity);
    ServiceResponse<bool> DeleteById(int id);
}