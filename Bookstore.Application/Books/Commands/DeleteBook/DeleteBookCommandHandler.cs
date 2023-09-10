using Bookstore.Application.Interfaces;
using MediatR;

namespace Bookstore.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBaseDbContext _context;

        public DeleteBookCommandHandler(IBaseDbContext context) =>
            _context = context;
        

        public async Task<Unit> Handle(DeleteBookCommand request, 
            CancellationToken cancellationToken)
        {
            var bookFromDb = await _context.Books.FindAsync(request.Id);
            
            if(bookFromDb is null)
            {
                throw new ArgumentNullException(nameof(bookFromDb));
            }

            _context.Books.Remove(bookFromDb);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
