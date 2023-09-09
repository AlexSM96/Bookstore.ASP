using Bookstore.Application.Interfaces;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.OrderServices
{
    public class OrderService : IBaseService<Order>
    {
        private readonly IBaseDbContext _context;

        public OrderService(IBaseDbContext context) => _context = context;

        public async Task<IList<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o=>o.User)
                .Include(o => o.Books)
                .ToListAsync();
        }

        public async Task CreateAsync(Order model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == model.UserId);

                if (user is null)
                {
                    throw new ArgumentNullException();
                }

                var order = await AddOrUpdateOrder(model);
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task UpdateAsync(Order model, CancellationToken cancellationToken)
        {
            try
            {
                var orderFromDb = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                if(orderFromDb is null)
                {
                    throw new ArgumentNullException();
                }

                var newOrder = await AddOrUpdateOrder(model, orderFromDb);
                _context.Orders.Update(newOrder);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (order is null)
                {
                    throw new ArgumentNullException();
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);
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
                    .FirstOrDefaultAsync(x=>x.Id == book.Id);
                AddBookToOrder(order, book, bookFromDb);
            }

            order.CreationDate = model.CreationDate;
            order.UserId = model.UserId;
            order.User = model.User;

            return order;
        }

        private void AddBookToOrder(Order order, Book book, Book? bookFromDb)
        {
            if(bookFromDb is null)
            {
                order.Books.Add(book);
                return;
            }

            order.Books.Add(bookFromDb);
        }

    }
}
