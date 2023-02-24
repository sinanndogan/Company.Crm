using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services;

public class OfferStatusService : IOfferStatusService
{
    private readonly IOfferStatusRepository _offerStatusRepository;
    public OfferStatusService(IOfferStatusRepository offerStatusRepository)
    {
        _offerStatusRepository = offerStatusRepository;
    }

    public ServiceResponse<List<OfferStatus>> GetAll()
    {
        var list = _offerStatusRepository.GetAll().ToList();
        return new ServiceResponse<List<OfferStatus>>(list);
    }

    public ServiceResponse<OfferStatus?> GetById(int id)
    {
        var data = _offerStatusRepository.GetById(id);
        if(data == null) return new ServiceResponse<OfferStatus?>("Not Found!");
        return new(data);
    }

    public ServiceResponse<OfferStatus> GetForEditById(int id)
    {
        var data = _offerStatusRepository.GetById(id);
        return new(data);
    }

    public ServicePaginationResponse<List<OfferStatus>> GetPaged(PaginationRequest request)
    {

        var query = _offerStatusRepository.GetAll();
        var total=query.Count();
        var pagedList = query.Skip(request.Skip).Take(request.PerPage).ToList();

        return new ServicePaginationResponse<List<OfferStatus>>(pagedList, total, request);
    }

    public ServiceResponse<bool> Insert(OfferStatus entity)
    {
        return new(_offerStatusRepository.Insert(entity));
    }

    public ServiceResponse<bool> Update(OfferStatus entity)
    {
        return new(_offerStatusRepository.Update(entity));
    }


    public ServiceResponse<bool> Delete(OfferStatus entity)
    {
        return new ServiceResponse<bool>(_offerStatusRepository.Delete(entity));
    }

    public ServiceResponse<bool> DeleteById(int id)
    {
        return new ServiceResponse<bool>(_offerStatusRepository.DeleteById(id));
    }
}