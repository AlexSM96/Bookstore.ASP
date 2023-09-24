namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.AddToBasket
{
    public class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommand, Basket>
    {
        private readonly IBaseDbContext _context;

        public AddToBasketCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Basket> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Basket)
                    .FirstOrDefaultAsync(b => b.Id == request.UserId);

                if (user is null)
                {
                    throw new ArgumentNullException(nameof(User));
                }

                var basket = await _context.Baskets
                    .Include(b => b.Books)
                    .FirstOrDefaultAsync(b => b.Id == user.Basket.Id);

                if (basket is null)
                {
                    basket.Id = Guid.NewGuid();
                    basket.UserId = user.Id;
                    basket.Books = request.Books;
                    await _context.Baskets.AddAsync(basket);
                }
                else
                {
                    foreach (var book in request.Books)
                    {
                        if (!basket.Books.Contains(book))
                        {
                            basket.Books.Add(book);
                        }
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);

                return basket;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
