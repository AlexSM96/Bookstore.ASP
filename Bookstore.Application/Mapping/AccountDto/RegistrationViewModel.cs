using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.AccountDto
{
    public class RegistrationViewModel : LoginViewModel
    {
        [Required(ErrorMessage = "Введите ваше имя")]
        public string Login { get; set; }
    }
}
