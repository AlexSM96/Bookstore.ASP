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
        public async Task<IActionResult> GetBooks()
        {
            var books = await _service
               .GetAllAsync(CancellationToken.None);
            return PartialView(_mapper.Map<IList<BookViewModel>>(books));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            var books = await _service
               .GetAllAsync(CancellationToken.None);
            var filteredBooks = books
                .SelectMany(x => x.Categories, (book, category) => new
                { Book = book, Category = category })
                .GroupBy(x => x.Category.Name, x => x.Book)
                .ToDictionary(x => x.Key, x => x.ToList());
            return View(_mapper.Map<IList<BookViewModel>>(filteredBooks[category]));
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredBooks(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index", "Book");
            }

            var books = await _service
                .GetAllAsync(CancellationToken.None);

            var filteredBooks = books
                .SelectMany(b => b.Authors, (book, author) => new
                { Book = book, Author = author })
                .GroupBy(x => x.Author.Name, x => x.Book)
                .ToDictionary(x => x.Key, x => x.ToList(),
                    StringComparer.OrdinalIgnoreCase);

            bool isContains = false;
            string newKey = "";
            foreach (var item in filteredBooks)
            {
                if (item.Key.Contains(key, StringComparison.OrdinalIgnoreCase))
                {
                    isContains = true;
                    newKey = item.Key;
                    break;
                }
            }

            if (filteredBooks.ContainsKey(key) || isContains)
            {
                return View(_mapper.Map<IList<BookViewModel>>(filteredBooks[newKey]));
            }
            else
            {
                filteredBooks[key] = books.Where(x => x.Title.Contains(key, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(_mapper.Map<IList<BookViewModel>>(filteredBooks[key]));
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var books = await _service
                .GetAllAsync(CancellationToken.None);
            var book = books.FirstOrDefault(b => b.Id == id);
            return View(_mapper.Map<BookViewModel>(book));
        }

        [HttpGet]
        public async Task<IActionResult> AddBook() =>
            PartialView(new BookViewModel());

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _service
                .CreateAsync(_mapper.Map<Book>(model), CancellationToken.None);
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _service.DeleteAsync(id, CancellationToken.None);
            return RedirectToAction("Index", "Admin");
        }
    }
}
