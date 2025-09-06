using Bookstore.Application.CommandAndQuery.Baskets.Commands.AddToBasket;
using Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteFromBasket;
using Bookstore.Application.CommandAndQuery.Baskets.Queries.GetBasket;

namespace Bookstore.Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketController(IMediator mediator, IMapper mapper) => (_mediator, _mapper) = (mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated
                && !string.IsNullOrWhiteSpace(User.Identity.Name))
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

        [HttpGet]
        public async Task<IActionResult> GetCountBooksInOrder()
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated
                && !string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                var user = await _mediator
                    .Send(new GetUserQuery<string>(User.Identity.Name));

                var basket = await _mediator
                    .Send(new GetBasketQuery(user.Id));
                return Json(new { Count = basket?.Books?.Count() });
            }

            return Json(new {});          
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(Guid bookId)
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated 
                && !string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                var book = await _mediator.Send(new GetBookByIdQuery(bookId));
                var user = await _mediator.Send(new GetUserQuery<string>(User.Identity.Name));


                var basket = new AddToBasketCommand
                {
                    UserId = user.Id,
                    Book = book
                };
                
                var response = await _mediator.Send(basket);

                return View();
            }

            return BadRequest();
        }


        public async Task<IActionResult> DeleteFromBasket(Guid bookId)
        {
            if (User is not null && User.Identity is not null && User.Identity.IsAuthenticated 
                && !string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                var user = await _mediator
                    .Send(new GetUserQuery<string>(User.Identity.Name));

                var deleteCommand = new DeleteFromBasketCommand()
                {
                    UserId = user.Id,
                    BookId = bookId
                };

                await _mediator.Send(deleteCommand);
            }

            return RedirectToAction(nameof(GetBasket));
        }
    }
}
