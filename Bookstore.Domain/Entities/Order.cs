using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTime CreationDate { get; set; } = DateTime.Today;

        public decimal TotalPrice { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public IList<Book> Books { get; set; } = new List<Book>();
    }
}
