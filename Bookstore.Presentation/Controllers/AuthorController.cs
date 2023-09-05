using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBaseService<Author> _service;
        private readonly IMapper _mapper;

        public AuthorController(IBaseService<Author> service, IMapper mapper) =>
            (_service, _mapper) = (service, mapper);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _service.GetAllAsync(CancellationToken.None);
            return View(_mapper.Map<IList<AuthorViewModel>>(authors));
        }

        [HttpGet]
        public async Task<IActionResult> GetJsonListAuthors()
        {
            var authors = await _service.GetAllAsync(CancellationToken.None);
            return Json(_mapper.Map<AuthorViewModel>(authors));
        }


        [HttpGet]
        public async Task<IActionResult> AddAuthor() => PartialView(new AuthorViewModel());

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _service
                .CreateAsync(_mapper.Map<Author>(model), CancellationToken.None);
            return Ok();
        }
    }
}
