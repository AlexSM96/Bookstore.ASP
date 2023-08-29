using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DAL.EntityConfiguration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasIndex(r => r.Id)
                .IsUnique();

            builder
                .Property(r => r.Text)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
