using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
