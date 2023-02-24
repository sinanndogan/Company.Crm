using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.Sale;

public class SaleDetailDto : BaseDto
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SaleAmount { get; set; }
    public string Description { get; set; }
}
