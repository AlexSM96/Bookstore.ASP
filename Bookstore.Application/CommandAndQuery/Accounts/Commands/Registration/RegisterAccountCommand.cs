using Bookstore.Domain.Enums;
using MediatR;
using System.Security.Claims;

namespace Bookstore.Application.CommandAndQuery.Accounts.Commands.Registration
{
    public class RegisterAccountCommand : IRequest<ClaimsIdentity>
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
