using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.CommandAndQuery.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler :
        IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IBaseDbContext _context;

        public UpdateBookCommandHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<Book> Handle(UpdateBookCommand request,
            CancellationToken cancellationToken)
        {
            var bookFromDb = await _context.Books
                .FindAsync(request.Id, cancellationToken);

            if (bookFromDb is null)
            {
                throw new ArgumentNullException(nameof(bookFromDb));
            }

            bookFromDb.PublicationDate = request.PublicationDate;
            bookFromDb.ImagePath = request.ImagePath;
            bookFromDb.Price = request.Price;
            bookFromDb.Title = request.Title;
            bookFromDb.Description = request.Description;

            foreach (var author in request.Authors)
            {
                var authorFromDb = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Name == author.Name);

                if (authorFromDb is null)
                {
                    bookFromDb.Authors.Add(author);
                }
                else
                {
                    bookFromDb.Authors.Add(authorFromDb);
                }
            }

            foreach (var category in request.Categories)
            {
                var categoryFromDb = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Name);

                if (categoryFromDb is null)
                {
                    bookFromDb.Categories.Add(category);
                }
                else
                {
                    bookFromDb.Categories.Add(categoryFromDb);
                }
            }

            _context.Books.Update(bookFromDb);
            await _context.SaveChangesAsync(cancellationToken);

            return bookFromDb;
        }
    }
}
