using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;

namespace Company.Crm.Application.Services.Abstracts;

public interface IOfferStatusService
{
    ServiceResponse<List<OfferStatus>> GetAll();
    ServiceResponse<OfferStatus?> GetById(int id);
    ServiceResponse<bool> Insert(OfferStatus entity);
    ServiceResponse<bool>Update(OfferStatus entity);
    ServiceResponse<bool>Delete(OfferStatus entity);
    ServiceResponse<bool>DeleteById(int id);
    ServicePaginationResponse<List<OfferStatus>> GetPaged(PaginationRequest request);

    ServiceResponse<OfferStatus> GetForEditById(int id);
}