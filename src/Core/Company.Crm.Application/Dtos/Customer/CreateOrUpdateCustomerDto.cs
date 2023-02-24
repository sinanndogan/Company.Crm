using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Crm.Application.Dtos;

public class CreateOrUpdateCustomerDto : BaseDto
{
    public int UserId { get; set; }

    public string? IdentityNumber { get; set; }

    public int? GenderId { get; set; }

    public int? TitleId { get; set; }

    public string CompanyName { get; set; }

    public int? StatusTypeId { get; set; }

    public int? RegionId { get; set; }

    public DateTime? BirthDate { get; set; }

    [NotMapped]
    [ValidateNever]
    public List<SelectListItem> Genders { get; set; }

    [NotMapped]
    [ValidateNever]
    public List<SelectListItem> Titles { get; set; }

    [NotMapped]
    [ValidateNever]
    public List<SelectListItem> StatusTypes { get; set; }
}