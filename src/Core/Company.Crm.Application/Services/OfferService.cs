using AutoMapper;
using Company.Crm.Application.Dtos;
using Company.Crm.Application.Dtos.Offer;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class OfferService : IOfferService
{
    private readonly IMapper _mapper;
    private readonly IOfferRepository _offerRepository;

    public OfferService(IOfferRepository offerService, IMapper mapper)
    {
        _offerRepository = offerService;
        _mapper = mapper;
    }

    public List<OfferDto> GetAll()
    {
        var entityList = _offerRepository.GetAll();
        var dtoList = _mapper.Map<List<OfferDto>>(entityList);
        return dtoList;
    }

    public OfferDto? GetById(int id)
    {
        var entity = _offerRepository.GetById(id);
        var dto = _mapper.Map<OfferDto>(entity);
        return dto;
    }

    public bool Insert(OfferDto dto)
    {
        var entity = _mapper.Map<Offer>(dto);
        return _offerRepository.Insert(entity);
    }

    public bool Update(OfferDto dto)
    {
        var entity = _mapper.Map<Offer>(dto);
        return _offerRepository.Update(entity);
    }

    public bool Delete(OfferDto dto)
    {
        var entity = _mapper.Map<Offer>(dto);
        return _offerRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _offerRepository.DeleteById(id);
    }
}