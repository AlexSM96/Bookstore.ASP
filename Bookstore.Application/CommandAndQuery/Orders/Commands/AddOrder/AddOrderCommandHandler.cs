using Bookstore.Application.Interfaces;
using Bookstore.Application.Mapping.BookDto;
using Bookstore.Application.Mapping.OrderDto;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.CommandAndQuery.Orders.Commands.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly IBaseDbContext _context;

        public AddOrderCommandHandler(IBaseDbContext context) =>
            _context = context;


        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == request.UserId);

                if (user is null)
                {
                    throw new ArgumentNullException();
                }

                var order = new Order
                {
                    CreationDate = request.CteationDate,
                    User = user,
                    UserId = request.UserId,
                    Books = request.Books
                };

                var newOrder = await AddOrUpdateOrder(order);

                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync(cancellationToken);

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
                AddBookToOrder(order, book, bookFromDb);
            }

            order.CreationDate = model.CreationDate;
            order.UserId = model.UserId;
            order.User = model.User;

            return order;
        }

        private void AddBookToOrder(Order order, Book book, Book? bookFromDb)
        {
            if (bookFromDb is null)
            {
                order.Books.Add(book);
                return;
            }

            order.Books.Add(bookFromDb);
        }

    }
}
