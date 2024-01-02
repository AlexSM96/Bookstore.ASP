namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.AddToBasket
{
    internal class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommand, Basket>
    {
        private readonly IBaseDbContext _context;

        public AddToBasketCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Basket> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                      .Include(u => u.Basket)
                      .FirstOrDefaultAsync(b => request != null && b.Id == request.UserId)
                      ?? throw new ArgumentNullException(nameof(User));

                var basket = await _context.Baskets
                    .Include(b => b.Books)
                    .FirstOrDefaultAsync(b => user != null && b.Id == user.Basket.Id);

                if (basket is null)
                {
                    basket = new Basket();
                    basket.Id = Guid.NewGuid();
                    basket.UserId = user.Id;
                    basket.Books.Add(request.Book);
                    await _context.Baskets.AddAsync(basket);
                }
                else
                {
                    if (!basket.Books.Contains(request.Book))
                    {
                        basket.Books.Add(request.Book);
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
