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
            return await _context.Orders.ToListAsync()
                ?? throw new ArgumentNullException();
        }

        public async Task CreateAsync(Order model, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == model.UserId);

                if (user is null)
                {
                    throw new ArgumentNullException();
                }

                await _context.Orders.AddAsync(model);
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
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                if(order is null)
                {
                    throw new ArgumentNullException();
                }

                _context.Orders.Update(model);
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
    }
}
