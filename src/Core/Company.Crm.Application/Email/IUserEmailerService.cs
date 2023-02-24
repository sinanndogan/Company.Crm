namespace Company.Crm.Application.Email;

public interface IUserEmailerService
{
    Task RegisterMailAsync(string email, string name);
    Task ConfirmationMailAsync(string link, string email);
    Task RemindPasswordMailAsync(string link, string email);
}