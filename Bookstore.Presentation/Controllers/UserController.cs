using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseService<User> _service;
        private readonly IMapper _mapper;

        public UserController(IBaseService<User> service, IMapper mapper) =>
            (_service, _mapper) = (service, mapper);

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service
                .GetAllAsync(CancellationToken.None);
            return PartialView(_mapper.Map<IList<UserViewModel>>(users));
        }

        public async Task<IActionResult> GetUser(Guid id)
        {
            var users = await _service
                .GetAllAsync(CancellationToken.None);
            var user = users
                .FirstOrDefault(x => x.Id == id);
            return View(_mapper.Map<UserViewModel>(user));
        }

        public async Task<IActionResult> GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var users = await _service
                .GetAllAsync(CancellationToken.None);
                var user = users
                    .FirstOrDefault(x => x.Login == User.Identity.Name);

                return Json(new {UserId = user?.Id, UserEmail = user?.Email});
            }

            return Json(default);
        }
    }
}
