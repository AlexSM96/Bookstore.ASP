using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public IList<Book> Books { get; set; }
    }
}
