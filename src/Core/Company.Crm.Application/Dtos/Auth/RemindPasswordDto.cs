using System.ComponentModel.DataAnnotations;

namespace Company.Crm.Application.Dtos;

public class RemindPasswordDto
{
    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string? EmailAddress { get; set; }
}