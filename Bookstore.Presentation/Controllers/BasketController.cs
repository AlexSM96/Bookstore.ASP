using Bookstore.Application.CommandAndQuery.Baskets.Commands.AddToBasket;
using Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteFromBasket;
using Bookstore.Application.CommandAndQuery.Baskets.Queries.GetBasket;
using Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksById;

namespace Bookstore.Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _mediator
                    .Send(new GetUserQuery<string>(User.Identity.Name));

                var basket = await _mediator
                    .Send(new GetBasketQuery(user.Id));

                var vm = _mapper.Map<IList<BookViewModel>>(basket?.Books);
                return View(vm);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(List<Guid> BooksId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var books = await _mediator.Send(new GetBooksByIdQuery(BooksId));
                var user = await _mediator.Send(new GetUserQuery<string>(User.Identity.Name));

                var basket = new AddToBasketCommand
                {
                    UserId = user.Id,
                    Books = books.ToList()
                };

                var response = await _mediator.Send(basket);

                return View(response);
            }

            return BadRequest();
        }

        public async Task<IActionResult> DeleteFromBasket(Guid bookId)
        {
            var user = await _mediator
                .Send(new GetUserQuery<string>(User.Identity.Name));

            var deleteCommand = new DeleteFromBasketCommand()
            {
                UserId = user.Id,
                BookId = bookId
            };

            await _mediator.Send(deleteCommand);
            return RedirectToAction("GetBasket");
        }
    }
}
