using System.ComponentModel.DataAnnotations;

namespace Bookstore.Test.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }
    }
}
