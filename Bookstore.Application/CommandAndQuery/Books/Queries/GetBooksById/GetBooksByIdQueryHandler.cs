namespace Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksById
{
    public class GetBooksByIdQueryHandler : IRequestHandler<GetBooksByIdQuery, IList<Book>>
    {
        private readonly IBaseDbContext _context;
        public GetBooksByIdQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<IList<Book>> Handle(GetBooksByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = new List<Book>();              
                if(request is not null)
                {
                    foreach (var bookId in request.BooksId)
                    {
                        var book = await _context.Books
                            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
                        if (book is not null)
                        {
                            books.Add(book);
                        }
                    }
                }
                    
                return books;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
