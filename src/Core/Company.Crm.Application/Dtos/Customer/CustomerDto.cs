using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos;

public class CustomerDto : BaseDto
{
    public int UserId { get; set; }
    public string? UserFullName { get; set; }
    public string? IdentityNumber { get; set; }
    public int? GenderId { get; set; }
    public string? GenderName { get; set; }
    public int? TitleId { get; set; }
    public string? TitleName { get; set; }
    public string CompanyName { get; set; }
    public DateTime BirthDate { get; set; }
    public int? StatusTypeId { get; set; }
    public string? StatusTypeName { get; set; }
}