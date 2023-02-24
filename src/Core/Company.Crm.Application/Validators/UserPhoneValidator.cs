using Company.Crm.Application.Dtos.UserPhone;
using FluentValidation;

namespace Company.Crm.Application.Validators;

public class UserPhoneValidator : AbstractValidator<CreateOrUpdateUserPhoneDto>
{
    public UserPhoneValidator()
    {
        RuleFor(t => t.UserId).NotNull();
        RuleFor(t => t.PhoneNumber).NotNull();
        RuleFor(t => t.PhoneType).NotNull();
    }
}