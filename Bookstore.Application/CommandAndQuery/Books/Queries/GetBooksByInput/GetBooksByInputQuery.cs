using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksByInput
{
    public record GetBooksByInputQuery(string InputData) : IRequest<IList<Book>>;
}
