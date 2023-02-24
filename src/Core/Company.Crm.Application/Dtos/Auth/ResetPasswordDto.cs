using System.ComponentModel.DataAnnotations;

namespace Company.Crm.Application.Dtos;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(50)]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(50)]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre Tekrarı")]
    [Compare("Password")]
    public string? PasswordRepeat { get; set; }
}