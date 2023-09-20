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
            var userName = "sAlexM74@yandex.ru";
            var password = "nsnxzhlgsursrmyv";
            using var message = new MimeMessage();
                    
            message.From.Add(new MailboxAddress("Bookstore",
                "sAlexM74@yandex.ru"));

            message.To.Add(new MailboxAddress("", email));

            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            await _client.ConnectAsync("smtp.yandex.ru", 465, true);
            await _client.AuthenticateAsync(userName, password);
            await _client.SendAsync(message);
            await _client.DisconnectAsync(true);
        }
    }
}
