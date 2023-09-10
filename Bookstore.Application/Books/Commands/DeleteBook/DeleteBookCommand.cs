using MediatR;

namespace Bookstore.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
