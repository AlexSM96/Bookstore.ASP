using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IBaseDbContext _context;

        public AddBookCommandHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<Book> Handle(AddBookCommand request, 
            CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                PublicationDate = request.PublicationDate,
                Description = request.Description,
                Price = request.Price,
                ImagePath = request.ImagePath
            };

            foreach (var author in request.Authors)
            {
                var authorFromDb = await _context.Authors
                    .FirstOrDefaultAsync(a=>a.Name == author.Name);

                if (authorFromDb is null)
                {
                    book.Authors.Add(author);
                }
                else
                {
                    book.Authors.Add(authorFromDb);
                }
            }

            foreach (var category in request.Categories)
            {
                var categoryFromDb = await _context.Categories
                    .FirstOrDefaultAsync(c=>c.Name == category.Name);

                if (categoryFromDb is null)
                {
                    book.Categories.Add(category);
                }
                else
                {
                    book.Categories.Add(categoryFromDb);
                }
            }

            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }
    }
}
