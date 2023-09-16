using Bookstore.Application.Extensions;
using Bookstore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bookstore.Application.CommandAndQuery.Accounts.Commands.LogIn
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, ClaimsIdentity>
    {
        private readonly IBaseDbContext _context;

        public LogInCommandHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<ClaimsIdentity> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email
                        && u.Password == request.Password);

                if (user is null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                return user.Authenticate();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
