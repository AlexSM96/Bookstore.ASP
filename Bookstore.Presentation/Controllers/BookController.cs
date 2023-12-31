﻿using Bookstore.Application.CommandAndQuery.Books.Commands.UpdateBook;

namespace Bookstore.Presentation.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BookController(IMapper mapper, IMediator mediator) => (_mapper, _mediator) = (mapper, mediator);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _mediator.Send(new GetBooksQuery());
            return View(_mapper.Map<IList<BookViewModel>>(books));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _mediator.Send(new GetBooksQuery());
            var booksVM = _mapper.Map<IList<BookViewModel>>(books);
            return View(booksVM);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest(new { Message = "Вы передали пустое значение" });
            }

            var books = await _mediator.Send(new GetBooksByInputQuery(category));
            return View("Index", _mapper.Map<IList<BookViewModel>>(books));
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredBooks(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return RedirectToAction("Index", "Book");
            }

            var books = await _mediator.Send(new GetBooksByInputQuery(key));
            return View("Index", _mapper.Map<IList<BookViewModel>>(books));
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(bookId));
            return View(_mapper.Map<BookViewModel>(book));
        }

        [HttpGet]
        public async Task<IActionResult> AddBook() => View(new BookViewModel());

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand model)
        {
            var book = await _mediator.Send(model, CancellationToken.None);
            if(book == null)
            {
                return BadRequest();
            }

            return RedirectToAction("AddBook", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(Guid bookId, UpdateBookCommand model)
        {
            model.Id = bookId;
            var book = await _mediator.Send(model, CancellationToken.None);
            if(book == null)
            {
                return BadRequest();
            }

            return RedirectToAction("GetBook", "Book", new { BookId = book.Id });
        }

        public async Task<IActionResult> DeleteBook(DeleteBookCommand model)
        {
            await _mediator.Send(model, CancellationToken.None);
            return RedirectToAction("GetBooks", "Book");
        }     
    }
}
