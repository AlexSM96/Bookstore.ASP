using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksById
{
    public record GetBooksByIdQuery(List<Guid> BooksId) : IRequest<IList<Book>>;   
}
