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
                var user = new User();
                var basket = new Basket();
                if (request is not null)
                {
                    user = await _context.Users
                        .Include(u => u.Basket)
                        .FirstOrDefaultAsync(b => b.Id == request.UserId)
                        ?? throw new ArgumentNullException(nameof(User));
                    basket = await _context.Baskets
                        .Include(b => b.Books)
                        .FirstOrDefaultAsync(b => b.Id == user.Basket.Id);

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
                }


                return basket;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
