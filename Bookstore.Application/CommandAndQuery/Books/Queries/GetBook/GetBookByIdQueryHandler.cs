namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBook
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBaseDbContext _context;
        public GetBookByIdQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var book = new Book();
                if (request is not null)
                {
                    book = await _context.Books
                        .Include(b => b.Categories)
                        .Include(b => b.Authors)
                        .FirstOrDefaultAsync(b => b.Id == request.Id);

                    if (book is null)
                    {
                        throw new ArgumentNullException(nameof(Book));
                    }
                }

                return book;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
