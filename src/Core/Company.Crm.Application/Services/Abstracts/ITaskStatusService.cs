using Company.Framework.Dtos;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Application.Services.Abstracts;

public interface ITaskStatusService
{
	ServiceResponse<List<TaskStatus>> GetAll();
	ServicePaginationResponse<List<TaskStatus>> GetPaged(PaginationRequest request);
	ServiceResponse<TaskStatus?> GetById(int id);
	ServiceResponse<bool> Insert(TaskStatus entity);
	ServiceResponse<bool> Update(TaskStatus entity);
	ServiceResponse<bool> Delete(TaskStatus entity);
	ServiceResponse<bool> DeleteById(int id);
	ServiceResponse<TaskStatus> GetForEditById(int id);
}