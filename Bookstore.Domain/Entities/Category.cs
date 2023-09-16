using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public IList<Book> Books { get; set; }
    }
}
