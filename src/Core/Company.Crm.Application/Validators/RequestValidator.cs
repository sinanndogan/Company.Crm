using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Application.Dtos.Request;
using FluentValidation;

namespace Company.Crm.Application.Validators;

public class RequestValidator : AbstractValidator<CreateOrUpdateRequestDto>
{
    public RequestValidator()
    {
        RuleFor(t => t.CustomerUserId).NotNull();
        RuleFor(t => t.Description).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(t => t.EmployeeUserId).NotNull();
    }
}