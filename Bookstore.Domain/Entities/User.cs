using Bookstore.Domain.Entities.Base;
using Bookstore.Domain.Enums;

namespace Bookstore.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public Basket Basket { get; set; }

        public IList<Review>? Reviews { get; set; }

        public IList<Order>? Orders { get; set; }
    }
}
