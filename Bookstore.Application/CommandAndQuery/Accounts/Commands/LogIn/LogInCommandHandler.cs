﻿namespace Bookstore.Application.CommandAndQuery.Accounts.Commands.LogIn
{
    internal class LogInCommandHandler : IRequestHandler<LogInCommand, ClaimsIdentity>
    {
        private readonly IBaseDbContext _context;

        public LogInCommandHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<ClaimsIdentity?> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

                return user?.Authenticate();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
