using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.CommandAndQuery.Users.Queries.GetUser
{
    public class GetUserQueryHandler<T> : IRequestHandler<GetUserQuery<T>, User>
    {
        private readonly IBaseDbContext _context;

        public GetUserQueryHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<User> Handle(GetUserQuery<T> request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Data)
                || x.Email.Equals(request.Data) || x.Login.Equals(request.Data));
        }
    }
}
