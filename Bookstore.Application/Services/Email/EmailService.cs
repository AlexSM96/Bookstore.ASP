using MailKit.Net.Smtp;
using MimeKit;

namespace Bookstore.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpClient _client;

        public EmailService(ISmtpClient client) => _client = client;

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var companyEmail = "sAlexM74@yandex.ru";
            var password = "Sam23620222@";
            using var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Адмиинстрация сайта",
                "sAlexM74@yandex.ru"));

            message.To.Add(new MailboxAddress("", email));

            message.Subject = subject;
            message.Body = new TextPart("Plain", body);

            await _client.ConnectAsync("smtp.yandex.ru", 25, false);
            await _client.AuthenticateAsync(companyEmail, password);
            await _client.SendAsync(message);
            await _client.DisconnectAsync(true);
        }
    }
}
