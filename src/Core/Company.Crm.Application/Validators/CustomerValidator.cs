using Company.Crm.Application.Dtos;
using FluentValidation;

namespace Company.Crm.Application.Validators;

public class CustomerValidator : AbstractValidator<CreateOrUpdateCustomerDto>
{
    public CustomerValidator()
    {
        RuleFor(t => t.UserId).NotNull().WithMessage("UserId is required!");
        RuleFor(t => t.CompanyName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(t => t.BirthDate).Must(BirthDateValidation);
    }

    private bool BirthDateValidation(DateTime? dt)
    {
        if (dt == null) return false;
        return dt.Value.Year >= 1950;
    }
}