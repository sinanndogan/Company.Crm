using Company.Framework.Entity;

namespace Company.Crm.Domain.Entities;

public class Sale : BaseEntity
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SaleAmount { get; set; }
    public string Description { get; set; }
}