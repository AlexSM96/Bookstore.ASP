using Bookstore.Application.Interfaces;
using Bookstore.Application.Mapping.AccountDto;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bookstore.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IBaseDbContext _context;

        public AccountService(IBaseDbContext context) =>
            _context = context;
        public async Task<ClaimsIdentity> RegisterAccountAsync(RegistrationViewModel model,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x=>x.Email == model.Email);

                if (user == null)
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        Login = model.Login,
                        Password = model.Password
                    };

                    AddRole(user);
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync(cancellationToken);
                    return AuthenticateUser(user);
                }

                throw new Exception($"[{nameof(user)}]: {model.Email} is already exist");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClaimsIdentity> LogInAsync(LoginViewModel model,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email 
                        && u.Password == model.Password);
                if(user is null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                return AuthenticateUser(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void AddRole(User user)
        {
            var role = user.Login == "salexm74@gmail.com"
                ? user.Role = Role.Admin : Role.User;
        }

        private ClaimsIdentity AuthenticateUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookies", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
