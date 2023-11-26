using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bookstore.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator) => _mediator = mediator;
        
        [HttpGet]
        public IActionResult RegisterUser() => View(new RegistrationViewModel());

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterAccountCommand model)
        {
            if (ModelState.IsValid) 
            {
                var identity = await _mediator.Send(model);

                if (identity is not null)
                {
                    await HttpContext
                        .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Home");
                }
            } 
            
            return View(new RegistrationViewModel());    
        }

        [HttpGet]
        public IActionResult SignIn() => View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> SignIn(LogInCommand model)
        {
            if (ModelState.IsValid)
            {
                var identity = await _mediator.Send(model);
                
                if (identity is not null)
                {
                    await HttpContext
                        .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", "Ползователь с таким Email не зарегистрирован");
                }
            }

            return View(new LoginViewModel());
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
