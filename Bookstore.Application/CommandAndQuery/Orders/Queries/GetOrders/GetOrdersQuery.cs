using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Orders.Queries.GetOrders
{
    public record GetOrdersQuery() : IRequest<IList<Order>>;
}
