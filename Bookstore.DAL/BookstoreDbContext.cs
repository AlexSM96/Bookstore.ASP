using Bookstore.Application.Interfaces;
using Bookstore.DAL.EntityConfiguration;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DAL
{
    public sealed class BookstoreDbContext 
        : DbContext, IBaseDbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> option)
            : base(option) { }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new BookConfiguration())
                .ApplyConfiguration(new AuthorConfiguration())
                .ApplyConfiguration(new OrderConfiguration())
                .ApplyConfiguration(new ReviewConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
