using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooks
{
    public record GetBooksQuery() : IRequest<IList<Book>>;
}
