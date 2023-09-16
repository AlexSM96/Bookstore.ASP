using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.CommandAndQuery.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IList<User>>
    {
        private readonly IBaseDbContext _context;

        public GetUsersQueryHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<IList<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(x => x.Reviews)
                .Include(x => x.Orders)
                .ThenInclude(x => x.Books)
                .ToListAsync(cancellationToken);
        }
    }
}
