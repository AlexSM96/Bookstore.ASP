namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksByInput
{
    public class GetBooksByInputQueryHandler : IRequestHandler<GetBooksByInputQuery, IList<Book>>
    {
        private readonly IBaseDbContext _context;
        public GetBooksByInputQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<IList<Book>> Handle(GetBooksByInputQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = new List<Book>();
                if (request is not null)
                {
                    books = await _context.Books
                        .Include(b => b.Authors)
                        .Include(b => b.Categories)
                        .Where(b => b.Categories
                            .Any(c => c.Name.Contains(request.InputData))
                            || b.Authors.Any(a => a.Name.Contains(request.InputData))
                            || b.Title.Contains(request.InputData))
                        .ToListAsync();
                }

                return books.Any() ? books : new List<Book>();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
