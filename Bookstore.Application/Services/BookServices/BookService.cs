using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Mapping.BookDto;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.BookServices
{
    public class BookService : IBaseService<BookViewModel>
    {
        private readonly IBookDbContext _context;
        private readonly IMapper _mapper;
        public BookService(IBookDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IList<BookViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books
                .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                ?? throw new ArgumentNullException(nameof(IList<BookViewModel>));
        }

        public async Task CreateAsync(BookViewModel model,
            CancellationToken cancellationToken)
        {
            try
            {
                var book = _mapper.Map<Book>(model);
                if (book is null) throw new ArgumentNullException(nameof(book));
                await _context.Books.AddAsync(book, cancellationToken);
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

        public async Task UpdateAsync(BookViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _context.Books
                    .FirstOrDefaultAsync(b => b.Id == model.Id);

                if (book is null || book.Id != model.Id)
                {
                    throw new ArgumentNullException();
                }
                    
                book = _mapper.Map<Book>(model);
                _context.Books.Update(book);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}
