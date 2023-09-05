using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DAL.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasIndex(b => b.Id)
                .IsUnique();

            builder
                .Property(b => b.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(b => b.ImagePath)
                .HasMaxLength(100);

            builder
                .Property(b => b.Description)
                .HasMaxLength(500);


        }
    }
}
