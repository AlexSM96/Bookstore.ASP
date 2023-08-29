﻿using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.DAL.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasIndex(c => c.Id)
                .IsUnique();

            builder
                .Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasOne(c => c.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(c => c.BookId);
        }
    }
}
