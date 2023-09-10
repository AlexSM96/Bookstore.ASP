using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.Books.Queries.GetBook
{
    public record GetBookByIdQuery(Guid Id) : IRequest<Book>;    
}
