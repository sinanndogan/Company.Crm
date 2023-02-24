using Company.Crm.Application.Dtos;
using Company.Crm.Domain.Entities.Usr;

namespace Company.Crm.Application.Services.Abstracts;

public interface IUserService
{
    List<User> GetAll();
    User? GetById(int id);
    bool Insert(User entity);
    bool Update(User entity);
    bool Delete(User id);
    bool DeleteById(int id);
    User? Login(LoginDto dto);
    User? Register(RegisterDto dto);
    bool ActivateUserByEmail(string email, string activationKey);
    User? GetByEmail(string email);
    void RemindPassword(RemindPasswordDto dto);
    void ResetPassword(ResetPasswordDto dto);
    Task<bool> IsUserExist(string username, string email);
    Task<bool> UpdateRefreshToken(int userId, string refreshToken, DateTime expireDate);
    Task<RefreshTokenUserDto?> GetUserByRefreshToken(string refreshToken);
    Task<bool> InsertAll(List<User> users);
}