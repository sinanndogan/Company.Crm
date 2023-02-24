using Company.Crm.Application.Dtos.List;

namespace Company.Crm.Application.Services.Abstracts;

public interface IGenderService
{
    List<GenderDto> GetAll();

    GenderDto? GetById(int id);

    bool Insert(GenderDto entity);

    bool Update(GenderDto entity);

    bool Delete(GenderDto entity);

    bool DeleteById(int id);
}