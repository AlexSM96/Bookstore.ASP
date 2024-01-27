using Bookstore.Test.Models;

namespace Bookstore.Test.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<User> GetUsers();

        public User GetUser(int id);

        void Create(User user);
    }
}
