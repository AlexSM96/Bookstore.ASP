namespace Bookstore.Application.CommandAndQuery.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBaseDbContext _context;
        public DeleteBookCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is not null)
                {
                    var bookFromDb = await _context.Books.FindAsync(request.Id, cancellationToken);

                    if (bookFromDb is null)
                    {
                        throw new ArgumentNullException(nameof(bookFromDb));
                    }
                    _context.Books.Remove(bookFromDb);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
            catch(Exception e)
            {
                throw e;
            }
            

            
        }
    }
}
