namespace Company.Crm.Application.Dtos.Sale;

public class SaleChartByYearDto
{
    public int Year { get; set; }
    public int SaleCount { get; set; }
    public decimal TotalSalePrice { get; set; }
}