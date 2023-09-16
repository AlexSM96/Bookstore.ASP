using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        public IList<Author> Authors { get; set; }

        public IList<Category> Categories { get; set; }
    }
}
