using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Company.Framework.Email;

public class EmailService : IEmailService
{
    private readonly SmtpClient _client;
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
        _client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = _settings.EnableSsl
        };
    }

    public Task SendAsync(string toEmail, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_settings.FromMail, _settings.FromName),
            Subject = subject,
            IsBodyHtml = true,
            Body = body
        };
        mail.To.Add(toEmail);

        return _client.SendMailAsync(mail);
    }

    public Task SendAsync(List<string> toEmails, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_settings.FromMail, _settings.FromName),
            Subject = subject,
            IsBodyHtml = true,
            Body = body
        };
        foreach (var toEmail in toEmails) mail.Bcc.Add(toEmail);

        return _client.SendMailAsync(mail);
    }
}