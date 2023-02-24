using Company.Crm.Application.Dtos.Sale;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface ISaleService
{
    List<SaleDetailDto> GetAll();
    List<SaleDetailDto> GetPaged(int page);
    SaleDetailDto? GetById(int id);
    bool Insert(CreateOrUpdateSaleDto entity);
    bool Update(CreateOrUpdateSaleDto entity);
    bool Delete(SaleDetailDto entity);
    bool DeleteById(int id);
    CreateOrUpdateSaleDto GetForEditById(int id);
    ServiceResponse<List<SaleChartByYearDto>> GetChartData();
}