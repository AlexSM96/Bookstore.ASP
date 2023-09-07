using Bookstore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.AccountDto
{
    public class LoginViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите ваш email")]
        [EmailAddress(ErrorMessage = "Некорректный email адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пороль")]
        [MinLength(8, ErrorMessage = "Пороль должен иметь длинну больше 8 симовлов")]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
