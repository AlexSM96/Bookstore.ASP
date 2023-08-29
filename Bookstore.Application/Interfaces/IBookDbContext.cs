using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Interfaces
{
    public interface IBookDbContext
    {
        public DbSet<Book> Books { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
