using AutoMapper;
using Company.Crm.Application.Dtos.Sale;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services;

public class SaleService : ISaleService
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public List<SaleDetailDto> GetAll()
    {
        var entityList = _saleRepository.GetAll();
        return entityList.Select(x => new SaleDetailDto
        {
            Id = x.Id,
            Description = x.Description,
            EmployeeUserId = x.EmployeeUserId,
            RequestId = x.RequestId,
            SaleAmount = x.SaleAmount,
            SaleDate = x.SaleDate
        }).ToList();
    }

    public List<SaleDetailDto> GetPaged(int page)
    {
        var entityList = _saleRepository.GetAll().OrderByDescending(c => c.Id);

        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();
        return _mapper.Map<List<SaleDetailDto>>(pagedList);
    }

    public SaleDetailDto? GetById(int id)
    {
        var entity = _saleRepository.GetById(id);
        return _mapper.Map<SaleDetailDto>(entity);
    }

    public bool Insert(CreateOrUpdateSaleDto entity)
    {
        var sale = _mapper.Map<Sale>(entity);
        return _saleRepository.Insert(sale);
    }

    public bool Update(CreateOrUpdateSaleDto entity)
    {
        var sale = _saleRepository.GetById(entity.Id);
        sale.RequestId = entity.RequestId;
        sale.EmployeeUserId = entity.EmployeeUserId;
        sale.SaleDate = entity.SaleDate;
        sale.SaleAmount = entity.SaleAmount;
        sale.Description = entity.Description;
        return _saleRepository.Update(sale);
    }

    public bool Delete(SaleDetailDto entity)
    {
        var sale = _mapper.Map<Sale>(entity);
        return _saleRepository.Delete(sale);
    }

    public bool DeleteById(int id)
    {
        return _saleRepository.DeleteById(id);
    }

    public CreateOrUpdateSaleDto GetForEditById(int id)
    {
        var sale = _saleRepository.GetById(id);
        return _mapper.Map<CreateOrUpdateSaleDto>(sale);
    }

    public ServiceResponse<List<SaleChartByYearDto>> GetChartData()
    {
        var data = _saleRepository.GetAll()
            .GroupBy(e => e.SaleDate.Year)
            .Select(x => new SaleChartByYearDto
            {
                Year = x.Key,
                SaleCount = x.Count(),
                TotalSalePrice = x.Sum(e => e.SaleAmount)
            })
            .ToList();

        // 2. yazım şekli
        //var data2 = from r in _saleRepository.GetAll()
        //    group r by r.SaleDate.Year into g
        //    select new 
        //    {
        //      Year = g.Key,
        //      SaleCount = g.Count()
        //    };

        return new(data);
    }
}