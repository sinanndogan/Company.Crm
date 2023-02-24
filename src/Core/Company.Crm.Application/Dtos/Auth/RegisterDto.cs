using System.ComponentModel.DataAnnotations;

namespace Company.Crm.Application.Dtos;

public class RegisterDto
{
    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(150)]
    [Display(Name = "Email")]
    public string? EmailAddress { get; set; }

    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(50)]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(50)]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrarı")]
    [Compare("Password")]
    public string? PasswordRepeat { get; set; }


    [Display(Name = "Ad")]
    public string? Name { get; set; }

    [Display(Name = "Soyad")]
    public string? Surname { get; set; }
}