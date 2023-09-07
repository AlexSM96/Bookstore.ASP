using AutoMapper;
using Bookstore.Application.Mapping.CategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBaseService<Category> _service;
        private readonly IMapper _mapper;

        public CategoryController(IBaseService<Category> service, IMapper mapper) =>
            (_service, _mapper) = (service, mapper);

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
            => PartialView(new CategoryViewModel());

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(model);
                await _service.CreateAsync(category,CancellationToken.None);
                return Ok();
            }

            return BadRequest(model);
        }
    }
}
