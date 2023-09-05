using AutoMapper;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.BookServices
{
    public class BookService : IBaseService<Book>
    {
        private readonly IBaseDbContext _context;
        
        public BookService(IBaseDbContext context, IMapper mapper) =>
            _context = context;

        public async Task<IList<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books
                .ToListAsync(cancellationToken)
                ?? throw new ArgumentNullException(nameof(IList<Book>));
        }

        public async Task CreateAsync(Book book,
            CancellationToken cancellationToken)
        {
            try
            {
                var newBook = await AddOrUpdateBook(book);

                if (newBook is null)
                {
                    throw new ArgumentNullException(nameof(newBook));
                }


                await _context.Books.AddAsync(newBook, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
        {
            try
            {
                var bookFromDb = await _context.Books
                    .FirstOrDefaultAsync(b => b.Id == book.Id);

                if (bookFromDb is null || bookFromDb.Id != book.Id)
                {
                    throw new ArgumentNullException();
                }

                bookFromDb = await AddOrUpdateBook(book, bookFromDb);

                _context.Books.Update(bookFromDb);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task DeleteAsync(Guid bookId, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _context.Books
                    .FirstOrDefaultAsync(b => b.Id == bookId);

                if (book is null)
                {
                    throw new ArgumentNullException(nameof(book));
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }


        private async Task<Book> AddOrUpdateBook(Book bookFromView, Book bookFromDb = null!)
        {
            var newBook = bookFromDb is not null ? bookFromDb : new Book();

            foreach (var author in bookFromView.Authors)
            {
                var authorFromDb = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Name == author.Name);
                AddAuthorToBook(newBook, author, authorFromDb);
            }

            foreach (var category in bookFromView.Categories)
            {
                var categoryFromDb = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Name);
                AddCategoryToBook(newBook, category, categoryFromDb);
            }

            newBook.Title = bookFromView.Title;
            newBook.PublicationDate = bookFromView.PublicationDate;
            newBook.ImagePath = bookFromView.ImagePath;
            newBook.Price = bookFromView.Price;
            newBook.Description = bookFromView.Description;

            return newBook;
        }

        private void AddCategoryToBook(Book newBook, Category category, Category? categoryFromDb)
        {
            if(categoryFromDb is null)
            {
                newBook.Categories.Add(category);
                return;
            }

            newBook.Categories.Add(categoryFromDb);
        }

        private void AddAuthorToBook(Book newBook, Author author, Author? authorFromDb)
        {
            if (authorFromDb is null)
            {
                newBook.Authors.Add(author);
                return;
            }

            newBook.Authors.Add(authorFromDb);
        }
    }
}
