namespace Bookstore.Presentation.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthorController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            var authorsVM = _mapper.Map<IList<AuthorViewModel>>(authors);
            return View(authorsVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetJsonListAuthors()
        {
            var authors = await _mediator.Send(new GetAuthorsQuery());
            var authorsVM = _mapper.Map<IList<AuthorViewModel>>(authors);
            return Json(authorsVM);
        }


        [HttpGet]
        public async Task<IActionResult> AddAuthor() => View(new AuthorViewModel());

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorCommand model)
        {
            if (!ModelState.IsValid) return View(model);

            var author = await _mediator.Send(model);
            if (author != null)
            {
                return Ok();
            }

            return BadRequest(model);
        }
    }
}
