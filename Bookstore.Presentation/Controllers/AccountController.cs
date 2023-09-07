using Bookstore.Application.Mapping.AccountDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookstore.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service) => _service = service;
        
        [HttpGet]
        public IActionResult RegisterUser() => View(new RegistrationViewModel());

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var claimsIdentity = await _service
                .RegisterAccountAsync(model, CancellationToken.None);

            if(claimsIdentity is not null)
            {
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }

            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult SignIn() => View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var claimsIdentity = await _service
                .LogInAsync(model, CancellationToken.None);

            if (claimsIdentity is not null)
            {
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }

            return BadRequest(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
