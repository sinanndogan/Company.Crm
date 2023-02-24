using Company.Crm.Application.Dtos;
using Company.Crm.Application.Dtos.Task;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface ITaskService
{

    ServiceResponse<List<TaskDto>> GetAll();
    ServiceResponse<TaskDto?> GetById(int id);
    ServiceResponse<CreateOrUpdateTaskDto?> GetForEditById(int id);
    ServiceResponse<bool> Insert(CreateOrUpdateTaskDto dto);
    ServiceResponse<bool> Update(CreateOrUpdateTaskDto dto);
    ServiceResponse<bool> Delete(TaskDto dto);
    ServiceResponse<bool> DeleteById(int id);
    ServicePaginationResponse<List<TaskDto>> GetPaged(PaginationRequest req);

}