using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBook
{
    public record GetBookByIdQuery(Guid Id) : IRequest<Book>;
}
