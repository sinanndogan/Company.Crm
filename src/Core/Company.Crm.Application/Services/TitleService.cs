using AutoMapper;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Repositories;

namespace Company.Crm.Application.Services;

public class TitleService : ITitleService
{
    private readonly IMapper _mapper;
    private readonly ITitleRepository _titleRepository;

    public TitleService(ITitleRepository titleRepository, IMapper mapper)
    {
        _titleRepository = titleRepository;
        _mapper = mapper;
    }

    public List<Title> GetAll()
    {
        var entityList = _titleRepository.GetAll().ToList();
        return entityList;
    }

    public Title? GetById(int id)
    {
        var entity = _titleRepository.GetById(id);
        return entity;
    }

    public bool Insert(Title entity)
    {
        return _titleRepository.Insert(entity);
    }

    public bool Update(Title entity)
    {
        return _titleRepository.Update(entity);
    }

    public bool Delete(Title entity)
    {
        return _titleRepository.Delete(entity);
    }

    public bool DeleteById(int id)
    {
        return _titleRepository.DeleteById(id);
    }

    public List<Title> GetPaged(int page = 1)
    {
        var entityList = _titleRepository.GetAll().OrderByDescending(c => c.Id);
        var pagedList = entityList.Skip((page - 1) * 10).Take(10).ToList();
        return pagedList;
    }
}