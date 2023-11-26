namespace Bookstore.Application.CommandAndQuery.Books.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IBaseDbContext _context;
        public AddBookCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = new Book();
                if (request is not null)
                {
                    book.Title = request.Title;
                    book.PublicationDate = request.PublicationDate;
                    book.Description = request.Description;
                    book.Price = request.Price;
                    book.ImagePath = request.ImagePath;

                    foreach (var author in request.Authors)
                    {
                        var authorFromDb = await _context.Authors
                            .FirstOrDefaultAsync(a => a.Name == author.Name);

                        book.Authors.Add(authorFromDb is null ? author : authorFromDb);
                    }

                    foreach (var category in request.Categories)
                    {
                        var categoryFromDb = await _context.Categories
                            .FirstOrDefaultAsync(c => c.Name == category.Name);

                        book.Categories.Add(categoryFromDb is null ? category : categoryFromDb);
                    }

                    await _context.Books.AddAsync(book, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
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
