using Microsoft.Extensions.Configuration;

namespace Bookstore.Application.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly ISmtpClient _client;
        private readonly IConfiguration _configuration;

        public EmailService(ISmtpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public string Email => _configuration.GetValueFromConfig("Site", nameof(Email));

        public string Password => _configuration.GetValueFromConfig("Site", nameof(Password));

        public async Task SendEmailAsync(string email, string subject, string body)
        {            
            using var message = new MimeMessage();
                    
            message.From.Add(new MailboxAddress("Bookstore", Email));

            message.To.Add(new MailboxAddress("", email));

            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body, 
            };

            await _client.ConnectAsync("smtp.yandex.ru", 465, true);
            await _client.AuthenticateAsync(Email, Password);
            await _client.SendAsync(message);
            await _client.DisconnectAsync(true);
        }
    }
}
