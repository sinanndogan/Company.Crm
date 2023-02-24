using AutoMapper;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class SettingService : ISettingService
{
    private readonly IMapper _mapper;
    private readonly ISettingRepository _settingRepository;

    public SettingService(ISettingRepository settingRepository, IMapper mapper)
    {
        _settingRepository = settingRepository;
        _mapper = mapper;
    }

    public List<Setting> GetAll()
    {
        var entityList = _settingRepository.GetAll().ToList();
        return entityList;
    }

    public Setting? GetById(int id)
    {
        var entity = _settingRepository.GetById(id);
        return entity;
    }

    public bool Insert(Setting entity)
    {
        return _settingRepository.Insert(entity);
    }

    public bool Update(Setting entity)
    {
        return _settingRepository.Update(entity);
    }

    public bool Delete(Setting entity)
    {
        return _settingRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _settingRepository.DeleteById(id);
    }

    public List<Setting> GetPaged(int page = 1)
    {
        var entityList = _settingRepository.GetAll().OrderByDescending(c => c.Id);
        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();
        return pagedList;
    }
}