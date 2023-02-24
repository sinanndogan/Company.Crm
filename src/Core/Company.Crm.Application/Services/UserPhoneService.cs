using AutoMapper;
using Company.Crm.Application.Dtos.UserPhone;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class UserPhoneService : IUserPhoneService
{
    private readonly IMapper _mapper;
    private readonly IUserPhoneRepository _userPhoneRepository;

    public UserPhoneService(IUserPhoneRepository userPhoneRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userPhoneRepository = userPhoneRepository;
    }

    public List<UserPhoneDto> GetAll()
    {
        var entities = _userPhoneRepository.GetAll().ToList();
        return _mapper.Map<List<UserPhoneDto>>(entities);
    }

    public UserPhoneDto? GetById(int id)
    {
        var entity = _userPhoneRepository.GetById(id);
        return _mapper.Map<UserPhoneDto>(entity);
    }

    public bool Insert(CreateOrUpdateUserPhoneDto dto)
    {
        var userPhone = _mapper.Map<UserPhone>(dto);
        return _userPhoneRepository.Insert(userPhone);
    }

    public bool Update(CreateOrUpdateUserPhoneDto dto)
    {
        var userPhone = _mapper.Map<UserPhone>(dto);
        return _userPhoneRepository.Update(userPhone);
    }

    public bool Delete(UserPhoneDto dto)
    {
        var userPhone = _mapper.Map<UserPhone>(dto);
        return _userPhoneRepository.Delete(userPhone);
    }

    public bool DeleteById(int id)
    {
        return _userPhoneRepository.DeleteById(id);
    }

    public List<UserPhoneDto> GetPaged(int page = 1)
    {
        var entityList = _userPhoneRepository.GetAll()
            .OrderByDescending(c => c.Id);

        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();

        var dtoList = _mapper.Map<List<UserPhoneDto>>(pagedList);

        return dtoList;
    }

    public CreateOrUpdateUserPhoneDto? GetForEditById(int id)
    {
        var entity = _userPhoneRepository.GetById(id);
        var dto = _mapper.Map<CreateOrUpdateUserPhoneDto>(entity);
        return dto;
    }
}