using Company.Crm.Application.Dtos.UserEmail;

namespace Company.Crm.Application.Services.Abstracts;

public interface IUserEmailService
{
    List<UserEmailDto> GetAll();
    List<UserEmailDto> GetPaged(int page = 1);
    UserEmailDto? GetById(int id);
    CreateOrUpdateUserEmailDto? GetForEditById(int id);
    bool Insert(CreateOrUpdateUserEmailDto dto);
    bool Update(CreateOrUpdateUserEmailDto dto);
    bool Delete(CreateOrUpdateUserEmailDto dto);
    bool DeleteById(int id);
}