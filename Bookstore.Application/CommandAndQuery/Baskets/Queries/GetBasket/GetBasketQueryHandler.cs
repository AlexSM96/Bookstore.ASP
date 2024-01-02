namespace Bookstore.Application.CommandAndQuery.Baskets.Queries.GetBasket
{
    public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Basket>
    {
        private readonly IBaseDbContext _context;

        public GetBasketQueryHandler(IBaseDbContext context) => _context = context;

        public async Task<Basket> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return await _context.Baskets
                .Include(b => b.Books)
                .FirstOrDefaultAsync(b => request != null && b.UserId == request.UserId, cancellationToken)
                 ?? throw new ArgumentNullException($"Корзина для пользователя c ID: [{request.UserId}] не найдена");
        }
    }
}
