using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class BookController : Controller
    {
        private readonly IBaseService<Book> _service;
        private readonly IMapper _mapper;

        public BookController(IBaseService<Book> service, IMapper mapper) =>
             (_service, _mapper) = (service, mapper);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _service
                .GetAllAsync(CancellationToken.None);
            return View(_mapper.Map<IList<BookViewModel>>(books));
        }

        [HttpGet]
        public async Task<IActionResult> AddBook() => PartialView(new BookViewModel());
        
        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            await _service
                .CreateAsync(_mapper.Map<Book>(model), CancellationToken.None);
            return Ok(model);
        }
    }
}
