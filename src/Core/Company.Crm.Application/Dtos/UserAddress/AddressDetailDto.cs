using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.Address;

public class AddressDetailDto : BaseDto
{
    public int UserId { get; set; }
    public string Description { get; set; }
    public string AddressType { get; set; }
}