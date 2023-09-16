using Bookstore.Domain.Entities;
using System.Security.Claims;

namespace Bookstore.Application.Extensions
{
    public static class AccountExtension
    {
        public static ClaimsIdentity Authenticate(this User user)
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
