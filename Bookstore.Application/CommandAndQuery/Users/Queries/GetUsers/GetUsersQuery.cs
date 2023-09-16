using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Users.Queries.GetUsers
{
    public record GetUsersQuery() : IRequest<IList<User>>;
}
