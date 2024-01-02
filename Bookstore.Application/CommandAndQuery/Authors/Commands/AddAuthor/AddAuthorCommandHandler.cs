namespace Bookstore.Application.CommandAndQuery.Authors.Commands.AddAuthor
{
    internal class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IBaseDbContext _context;

        public AddAuthorCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newAuthor = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Name == request.Name);

                if (newAuthor is not null)
                {
                    throw new Exception($"Автор: Name = {request.Name}");
                }

                newAuthor = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                };

                await _context.Authors.AddAsync(newAuthor);
                await _context.SaveChangesAsync(cancellationToken);

                return newAuthor;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
