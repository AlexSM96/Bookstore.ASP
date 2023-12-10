namespace Bookstore.Application.CommandAndQuery.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IBaseDbContext _context;
        public UpdateBookCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookFromDb = new Book();
                if (request is not null)
                {
                    bookFromDb = await _context.Books
                        .Include(x=>x.Authors)
                        .Include(x=>x.Categories)
                        .FirstOrDefaultAsync(x=>x.Id == request.Id, cancellationToken);

                    if (bookFromDb is null)
                    {
                        throw new ArgumentNullException(nameof(Book));
                    }

                    bookFromDb.PublicationDate = request.PublicationDate;
                    bookFromDb.ImagePath = request.ImagePath;
                    bookFromDb.Price = request.Price;
                    bookFromDb.Title = request.Title;
                    bookFromDb.Description = request.Description;

                    foreach (var authorFromRequest in request.Authors)
                    {
                        var authorFromDb = await _context.Authors
                            .FirstOrDefaultAsync(a => a.Name == authorFromRequest.Name);
                        bookFromDb.Authors.Add(authorFromDb is null ? authorFromRequest : authorFromDb);
                    }

                    foreach (var categoryFromRequest in request.Categories)
                    {
                        var categoryFromDb = await _context.Categories
                            .FirstOrDefaultAsync(c => c.Name == categoryFromRequest.Name);
                        bookFromDb.Categories.Add(categoryFromDb is null ? categoryFromRequest : categoryFromDb);
                    }

                    _context.Books.Update(bookFromDb);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return bookFromDb;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
