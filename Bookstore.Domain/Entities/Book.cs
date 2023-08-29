using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImagePath { get; set; }

        public IList<Author> Authors { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Review>? Reviews { get; set; }

        public IList<Order>? Orders { get; set; }
    }
}
