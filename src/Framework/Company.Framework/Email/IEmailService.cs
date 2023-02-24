namespace Company.Framework.Email;

public interface IEmailService
{
    Task SendAsync(string toEmail, string subject, string body);

    Task SendAsync(List<string> toEmails, string subject, string body);
}