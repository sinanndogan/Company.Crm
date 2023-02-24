using System.ComponentModel.DataAnnotations.Schema;
using Company.Crm.Domain.Enums;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Crm.Application.Dtos.UserEmail;

public class CreateOrUpdateUserEmailDto : BaseDto
{
    public CreateOrUpdateUserEmailDto()
    {
        EmailTypes = new List<SelectListItem>();
    }

    public int UserId { get; set; }
    public string EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }

    [NotMapped]
    [ValidateNever]
    public List<SelectListItem>? EmailTypes { get; set; }
}