namespace Company.Crm.Application.Dtos;

public class RefreshTokenUserDto
{
    public int Id { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpireDate { get; set; }
}