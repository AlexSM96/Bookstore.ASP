using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Interfaces
{
    public interface IAuthorDbContext
    {
        public DbSet<Author> Authors { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
