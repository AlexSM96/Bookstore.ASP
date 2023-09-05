using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DAL.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Name)
                .IsRequired();
            builder
                .HasMany(c => c.Books)
                .WithMany(x => x.Categories);
        }
    }
}
