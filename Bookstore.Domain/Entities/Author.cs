using Bookstore.Domain.Entities.Base;

namespace Bookstore.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public IList<Book> Books { get; set; }
    }
}
