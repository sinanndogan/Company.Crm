using Company.Crm.Application.Dtos.Task;
using FluentValidation;

namespace Company.Crm.Application.Validators;

public class TaskValidator : AbstractValidator<CreateOrUpdateTaskDto>
{
    public TaskValidator()
    {
        RuleFor(t => t.RequestId).NotNull();
        RuleFor(t => t.EmployeeUserId).NotNull();
        RuleFor(t => t.TaskStatusId).NotNull();
        RuleFor(t => t.Description).NotNull().NotEmpty().MaximumLength(500);
        RuleFor(t => t.TaskStartDate).NotEmpty();
        RuleFor(t => t.TaskEndDate).NotEmpty().GreaterThan(t => t.TaskStartDate);
    }
}