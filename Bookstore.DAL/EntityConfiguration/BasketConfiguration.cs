using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DAL.EntityConfiguration
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {

        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .HasMany(b => b.Books)
                .WithMany(book => book.Baskets);

            builder
                .HasOne(b => b.User)
                .WithOne(u => u.Basket);
        }
    }
}
