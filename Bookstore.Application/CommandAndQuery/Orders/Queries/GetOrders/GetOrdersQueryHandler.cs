using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.CommandAndQuery.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IList<Order>>
    {
        private readonly IBaseDbContext _context;

        public GetOrdersQueryHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<IList<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ToListAsync(cancellationToken);
        }
    }
}
