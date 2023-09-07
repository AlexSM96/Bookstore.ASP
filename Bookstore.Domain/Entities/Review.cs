using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Review : BaseEntity
    {
        public string Text { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Today;

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
