using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksByInput
{
    public class GetBooksByInputQueryHandler :
        IRequestHandler<GetBooksByInputQuery, IList<Book>>
    {
        private readonly IBaseDbContext _context;

        public GetBooksByInputQueryHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<IList<Book>> Handle(GetBooksByInputQuery request,
            CancellationToken cancellationToken)
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .Where(b => b.Categories
                    .Any(c => c.Name.Contains(request.InputData))
                    || b.Authors.Any(a => a.Name.Contains(request.InputData))
                    || b.Title.Contains(request.InputData))
                .ToListAsync();

            if (!books.Any())
            {
                return new List<Book>();
            }

            return books;
        }
    }
}
