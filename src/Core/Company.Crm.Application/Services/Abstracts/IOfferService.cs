using Company.Crm.Application.Dtos;
using Company.Crm.Application.Dtos.Offer;

namespace Company.Crm.Application.Services.Abstracts;

public interface IOfferService
{
    List<OfferDto> GetAll();
    OfferDto? GetById(int id);
    bool Insert(OfferDto entity);
    bool Update(OfferDto entity);
    bool Delete(OfferDto entity);
    bool DeleteById(int id);
}