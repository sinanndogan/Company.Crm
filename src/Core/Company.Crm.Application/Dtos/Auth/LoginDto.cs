using System.ComponentModel.DataAnnotations;

namespace Company.Crm.Application.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(150)]
    [Display(Name = "Email/Kullanıcı Adı")]
    public string? EmailAddressOrUsername { get; set; }

    [Required(ErrorMessage = "Bu alan gerekli")]
    [StringLength(50)]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string? Password { get; set; }

    [Display(Name = "Beni Hatırla")]
    public bool RememberMe { get; set; }
}