namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBook
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBaseDbContext _context;
        public GetBookByIdQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Books
                        .Include(b => b.Categories)
                        .Include(b => b.Authors)
                        .FirstOrDefaultAsync(b => b.Id == request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
