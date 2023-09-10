using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IList<Book>>
    {
        private readonly IBaseDbContext _context;

        public GetBooksQueryHandler(IBaseDbContext context) =>
            _context = context;


        public async Task<IList<Book>> Handle(GetBooksQuery request,
            CancellationToken cancellationToken)
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Reviews)
                .ToListAsync(cancellationToken);

            if (!books.Any())
            {
                throw new ArgumentNullException(nameof(books));
            }

            return books;
        }
    }
}
