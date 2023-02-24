using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Entities.Usr;
using Company.Framework.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Domain.Entities;

public class Task : BaseEntity
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime TaskStartDate { get; set; }
    public DateTime TaskEndDate { get; set; }
    public string? Description { get; set; }
    public int TaskStatusId { get; set; }

    #region Navigation Properties

    [ForeignKey("RequestId")]
    public Request? RequestFK { get; set; }

    [ForeignKey("EmployeeUserId")]
    public Employee? EmployeeFK { get; set; }

    [ForeignKey("TaskStatusId")]
    public TaskStatus? TaskStatusFK { get; set; }

    #endregion
}