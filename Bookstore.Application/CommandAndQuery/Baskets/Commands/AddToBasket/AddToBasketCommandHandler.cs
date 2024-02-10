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
                      .FirstOrDefaultAsync(u => request != null && u.Id == request.UserId);

                var basket = await _context.Baskets
                    .Include(b => b.Books)
                    .FirstOrDefaultAsync(b => user != null && user.Basket != null && b.Id == user.Basket.Id);

                if (basket is null)
                {
                    basket = new Basket();
                    basket.Id = Guid.NewGuid();
                    basket.UserId = user.Id;
                    if(basket.Books == null)
                    {
                        basket.Books = new List<Book>();
                    }

                    basket.Books.Add(request.Book);
                    await _context.Baskets.AddAsync(basket);
                }
                else
                {
                    if (basket.Books != null && !basket.Books.Contains(request.Book))
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
