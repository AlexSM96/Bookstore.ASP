using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseService<User> _service;

        public UserController(IBaseService<User> service) =>
            _service = service;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUser(Guid id)
        {
            var users = await _service
                .GetAllAsync(CancellationToken.None);
            var user = users
                .FirstOrDefault(u => u.Id == id);
            return Json(user);
        }
    }
}
