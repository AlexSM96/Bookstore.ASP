namespace Bookstore.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            var usersVM = _mapper.Map<IList<UserViewModel>>(users);
            return PartialView(usersVM);
        }

        public async Task<IActionResult> GetUser()
        {
            var user = await _mediator
                .Send(new GetUserQuery<string>(User.Identity.Name));

            var userVM = _mapper.Map<UserViewModel>(user);
            return View(userVM);
        }

        public async Task<IActionResult> GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _mediator
                    .Send(new GetUserQuery<string>(User.Identity.Name));

                return Json(new {UserId = user?.Id, UserEmail = user?.Email});
            }

            return Json(default);
        }
    }
}
