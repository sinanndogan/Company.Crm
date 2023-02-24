using Company.Crm.Domain.Entities;

namespace Company.Crm.Application.Services.Abstracts;

public interface ISettingService
{
    List<Setting> GetAll();
    Setting? GetById(int id);
    bool Insert(Setting entity);
    bool Update(Setting entity);
    bool Delete(Setting entity);
    bool DeleteById(int id);
    List<Setting> GetPaged(int page = 1);
}