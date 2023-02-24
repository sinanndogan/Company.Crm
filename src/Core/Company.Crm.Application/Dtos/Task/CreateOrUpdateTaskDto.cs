using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.Task;

public class CreateOrUpdateTaskDto : BaseDto
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime TaskStartDate { get; set; }
    public DateTime TaskEndDate { get; set; }
    public string Description { get; set; }
    public int TaskStatusId { get; set; }
}