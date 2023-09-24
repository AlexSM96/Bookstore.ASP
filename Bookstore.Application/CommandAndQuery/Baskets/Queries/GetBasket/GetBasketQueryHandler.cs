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
                .FirstOrDefaultAsync(b => b.UserId == request.UserId);
        }
    }
}
