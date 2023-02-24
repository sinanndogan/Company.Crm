using Company.Crm.Domain.Entities.Lst;

namespace Company.Crm.Application.Services.Abstracts;

public interface ITitleService
{
    List<Title> GetAll();
    List<Title> GetPaged(int page = 1);
    Title? GetById(int id);
    bool Insert(Title entity);
    bool Update(Title entity);
    bool Delete(Title entity);
    bool DeleteById(int id);
}