using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.UserPhone;

public class UserPhoneDto : BaseDto
{
    public int UserId { get; set; }
    public string? PhoneNumber { get; set; }
    public int PhoneType { get; set; }
}