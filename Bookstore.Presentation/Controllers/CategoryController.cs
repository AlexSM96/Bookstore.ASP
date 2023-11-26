namespace Bookstore.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper) => (_mediator, _mapper) = (mediator, mapper);

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());
            var categoriesVM = _mapper.Map<IList<CategoryViewModel>>(categories);
            return PartialView(categoriesVM);
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory() => View(new CategoryViewModel());

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryCommand model)
        {
            if (ModelState.IsValid)
            {
                var category = await _mediator.Send(model);
                return Ok();
            }

            return BadRequest(model);
        }
    }
}
