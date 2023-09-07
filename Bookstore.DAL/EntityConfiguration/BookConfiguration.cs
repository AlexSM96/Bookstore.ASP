using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DAL.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder
                .Property(b => b.Title)
                .HasMaxLength(1000)
                .IsRequired();

            builder
                .Property(b => b.ImagePath)
                .HasMaxLength(1000);

            builder
                .Property(b => b.Description)
                .HasMaxLength(1000);
        }
    }
}
