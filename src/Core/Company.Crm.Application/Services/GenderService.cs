using AutoMapper;
using Company.Crm.Application.Dtos.List;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class GenderService : IGenderService
{
    private readonly IGenderRepository _genderRepository;
    private readonly IMapper _mapper;

    public GenderService(IGenderRepository genderRepository, IMapper mapper)
    {
        _genderRepository = genderRepository;
        _mapper = mapper;
    }

    public List<GenderDto> GetAll()
    {
        var genderList = _genderRepository.GetAll();
        var genderDtoList = _mapper.Map<List<GenderDto>>(genderList);

        return genderDtoList;
    }

    public GenderDto? GetById(int id)
    {
        var gender = _genderRepository.GetById(id);

        var genderDto = _mapper.Map<GenderDto>(gender);

        return genderDto;
    }

    public bool Insert(GenderDto model)
    {
        var entity = _mapper.Map<Gender>(model);

        return _genderRepository.Insert(entity);
    }

    public bool Update(GenderDto model)
    {
        var entity = _mapper.Map<Gender>(model);

        return _genderRepository.Update(entity);
    }
    
    public bool Delete(GenderDto model)
    {
        var entity = _mapper.Map<Gender>(model);

        return _genderRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _genderRepository.DeleteById(id);
    }
}