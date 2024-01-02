namespace Bookstore.Application.CommandAndQuery.Orders.Commands.AddOrder
{
    internal class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly IBaseDbContext _context;
        public AddOrderCommandHandler(IBaseDbContext context) => _context = context;
        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newOrder = new Order();
                if (request is not null)
                {
                    var user = await _context.Users
                        .FirstOrDefaultAsync(x => x.Id == request.UserId);

                    if (user is null)
                    {
                        throw new ArgumentNullException();
                    }

                    var order = new Order
                    {
                        CreationDate = request.CreationDate,
                        User = user,
                        UserId = request.UserId,
                        Books = request.Books
                    };

                    newOrder = await AddOrUpdateOrder(order);
                    await _context.Orders.AddAsync(newOrder);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return newOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Order> AddOrUpdateOrder(Order model, Order orderFromDb = null)
        {
            var order = orderFromDb is null ? new Order() : orderFromDb;

            foreach (var book in model.Books)
            {
                var bookFromDb = await _context.Books
                    .FirstOrDefaultAsync(x => x.Id == book.Id);
                order.Books.Add(bookFromDb is null ? book : bookFromDb);
            }

            order.CreationDate = model.CreationDate;
            order.UserId = model.UserId;
            order.User = model.User;

            return order;
        }
    }
}
