using Bookstore.Application.Extensions;
using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bookstore.Application.CommandAndQuery.Accounts.Commands.Registration
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, ClaimsIdentity>
    {
        private readonly IBaseDbContext _context;

        public RegisterAccountCommandHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<ClaimsIdentity> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == request.Email);

                if (user == null)
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = request.Email,
                        Login = request.Login,
                        Password = request.Password,
                    };

                    user.Role = AddRole(user);
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync(cancellationToken);
                    return user.Authenticate();
                }

                throw new Exception($"[{nameof(user)}]: {request.Email} is already exist");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Role AddRole(User user)
        {
            return user.Email == "salexm74@gmail.com" ? Role.Admin : Role.User;
        }
    }
}
