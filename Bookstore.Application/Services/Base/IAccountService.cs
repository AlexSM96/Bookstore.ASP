using Bookstore.Application.Mapping.AccountDto;
using System.Security.Claims;

namespace Bookstore.Application.Services.Base
{
    public interface IAccountService
    {
        public Task<ClaimsIdentity> RegisterAccountAsync
            (RegistrationViewModel model, CancellationToken cancellationToken);

        public Task<ClaimsIdentity> LogInAsync
            (LoginViewModel model, CancellationToken cancellationToken);
    }
}
