namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IList<Book>>
    {
        private readonly IBaseDbContext _context;
        public GetBooksQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<IList<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken) =>
            await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Reviews)
                .Include(b => b.Categories)
                .ToListAsync(cancellationToken);
           
    }
}
