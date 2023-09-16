using Bookstore.Domain.Enums;
using MediatR;
using System.Security.Claims;

namespace Bookstore.Application.CommandAndQuery.Accounts.Commands.LogIn
{
    public class LogInCommand : IRequest<ClaimsIdentity>
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
