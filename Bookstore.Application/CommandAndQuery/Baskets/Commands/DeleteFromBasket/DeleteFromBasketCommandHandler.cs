﻿namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteFromBasket
{
    internal class DeleteFromBasketCommandHandler : IRequestHandler<DeleteFromBasketCommand, Unit>
    {
        private readonly IBaseDbContext _context;

        public DeleteFromBasketCommandHandler(IBaseDbContext context) => _context = context;
        
        public async Task<Unit> Handle(DeleteFromBasketCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var basket = await _context.Baskets
                    .Include(b => b.Books)
                    .FirstOrDefaultAsync(b => b.UserId == request.UserId);

                var book = await _context.Books
                    .FirstOrDefaultAsync(x => x.Id == request.BookId);

                if(book is not null)
                {
                    basket?.Books?.Remove(book);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
