using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Crm.Entityframework.Repositories;
using Company.Framework.Dtos;
using Microsoft.EntityFrameworkCore;


namespace Company.Crm.Application.Services;

public class RegionService : IRegionService
{
    private readonly IRegionRepository _regionRepository;

    public RegionService(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public ServiceResponse<List<Region>> GetAll()
    {

        var entity = _regionRepository.GetAll().ToList();
        return new(entity);

    }


    public ServiceResponse<Region?> GetById(int id)
    {
        var entity = _regionRepository.GetById(id);
        if (entity == null)
            return new ServiceResponse<Region?>("Not Found!");
        return new(entity);
    }


    public ServiceResponse<bool> Insert(Region entity)
    {
        return new(_regionRepository.Insert(entity));
    }


    public ServiceResponse<Region> GetForEditById(int id)
    {
        var entity = _regionRepository.GetById(id);
        return new(entity);
    }


    public ServiceResponse<bool> Update(Region entity)
    {
        return new(_regionRepository.Update(entity));
    }


    public ServiceResponse<bool> Delete(Region entity)
    {
        return new ServiceResponse<bool>(_regionRepository.Delete(entity));
    }


    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_regionRepository.DeleteById(id));
    }

     public ServicePaginationResponse<List<Region>> GetPaged(PaginationRequest request)
    {      

        var entityQuery = _regionRepository.GetAll()                              
                   .OrderByDescending(c => c.Id);

        var totalCount = entityQuery.Count();

        var pagedList = entityQuery.Skip(request.Skip).Take(request.PerPage);

        var data = entityQuery.ToList();

        return new ServicePaginationResponse<List<Region>>(data, totalCount, request);



    }

}
