using Bookstore.Application.Mapping.BookDto;
using Bookstore.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class BookController : Controller
    {
        private readonly IBaseService<BookViewModel> _service;

        public BookController(IBaseService<BookViewModel> service) =>
             _service = service;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _service.GetAllAsync(CancellationToken.None);
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> AddBook() => View(new BookViewModel());
        
        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _service.CreateAsync(model, CancellationToken.None);
            return Ok(model);
        }
    }
}
