using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Interfaces
{
    public interface IBaseDbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
