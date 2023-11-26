namespace Bookstore.Application.Services.Email
{
    public interface IEmailService
    {
        public string Email { get; }

        public string Password { get; }

        public Task SendEmailAsync(string email, string subject, string body);
    }
}