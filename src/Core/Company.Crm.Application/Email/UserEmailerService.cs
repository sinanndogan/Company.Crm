using Company.Framework.Email;
using Microsoft.Extensions.Options;

namespace Company.Crm.Application.Email;

public class UserEmailerService : EmailService, IUserEmailerService
{
    public UserEmailerService(IOptions<EmailSettings> options) : base(options)
    {
    }

    public async Task RegisterMailAsync(string email, string name)
    {
        var subject = "Company.Crm - Register";
        var body = @$"
            <h2>Hello {name}, you are registered successfully!</h2><hr/>
        ";

        await SendAsync(email, subject, body);
    }

    public async Task ConfirmationMailAsync(string link, string email)
    {
        var subject = "Company.Crm - Confirmation Email";
        var body = @$"
            <h2>Please click this link for confirm email</h2><hr/>
            <a href='{link}'>Confirm Email</a>
        ";
        await SendAsync(email, subject, body);
    }

    public async Task RemindPasswordMailAsync(string link, string email)
    {
        var subject = "Company.Crm - Reset Password";
        var body = @$"
            <h2>Please click this link for reset password</h2><hr/>
            <a href='{link}'>Reset Password</a>
        ";
        await SendAsync(email, subject, body);
    }
}