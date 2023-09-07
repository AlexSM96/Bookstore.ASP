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

        public IList<Author> Authors { get; set; } = new List<Author>();

        public IList<Category> Categories { get; set; } = new List<Category>();

        public IList<Review>? Reviews { get; set; } = new List<Review>();

        public IList<Order>? Orders { get; set; } = new List<Order>();
    }
}
