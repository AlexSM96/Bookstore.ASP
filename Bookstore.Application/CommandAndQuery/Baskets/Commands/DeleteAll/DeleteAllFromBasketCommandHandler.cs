
namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteAll
{
    internal class DeleteAllFromBasketCommandHandler : IRequestHandler<DeleteAllFromBasketCommand, Unit>
    {
        private readonly IBaseDbContext _context;

        public DeleteAllFromBasketCommandHandler(IBaseDbContext context) => _context = context;
        
        public async Task<Unit> Handle(DeleteAllFromBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var basket = await _context.Baskets
                    .Include(x => x.Books)
                    .FirstOrDefaultAsync(x => x.UserId == request.UserId);

                basket?.Books?.Clear();
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
