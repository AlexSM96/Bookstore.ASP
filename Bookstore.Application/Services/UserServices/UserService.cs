using Bookstore.Application.Interfaces;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.UserServices
{
    public class UserService : IBaseService<User>
    {
        private readonly IBaseDbContext _context;

        public UserService(IBaseDbContext context) => _context = context;

        public async Task<IList<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(x => x.Reviews)
                .Include(x => x.Orders)
                .ToListAsync(cancellationToken);
        }

        public Task CreateAsync(User model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user is null)
                {
                    throw new ArgumentNullException();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync(CancellationToken.None);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
