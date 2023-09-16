using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Authors.Queries.GetAuthors
{
    public record GetAuthorsQuery() : IRequest<IList<Author>>;
}
