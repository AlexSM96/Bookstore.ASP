using Bookstore.Application.Interfaces;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.ReviewServices
{
    public class ReviewService : IBaseService<Review>
    {
        private readonly IBaseDbContext _context;

        public ReviewService(IBaseDbContext context) => _context = context;

        public async Task<IList<Review>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync(cancellationToken)
                ?? throw new ArgumentNullException();
        }

        public async Task CreateAsync(Review model, CancellationToken cancellationToken)
        {
            try
            {
                if (model is null)
                {
                    throw new ArgumentNullException();
                }

                await _context.Reviews.AddAsync(model);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task UpdateAsync(Review model, CancellationToken cancellationToken)
        {
            try
            {
                throw new Exception();
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
                var comment = await _context.Reviews
                    .FirstOrDefaultAsync(r => r.Id == id);

                if(comment is null)
                {
                    throw new ArgumentNullException();
                }

                _context.Reviews.Remove(comment);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
