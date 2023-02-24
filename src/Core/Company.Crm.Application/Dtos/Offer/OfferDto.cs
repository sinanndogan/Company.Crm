using Company.Framework.Dtos;

namespace Company.Crm.Application.Dtos.Offer;

public class OfferDto : BaseDto
{
    public int RequestId { get; set; }
    public int EmployeeUserId { get; set; }
    public DateTime? OfferDate { get; set; }
    public decimal BidAmount { get; set; }
    public int OfferStatusId { get; set; }
}