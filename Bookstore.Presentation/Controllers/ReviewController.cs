using Bookstore.Application.CommandAndQuery.Reviews.Commands.DeleteReview;

namespace Bookstore.Presentation.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReviewController(IMediator mediator, IMapper mapper) => (_mediator, _mapper) = (mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetCommentsForAdmin()
        {
            var reviews = await _mediator.Send(new GetReviewsQuery());
            var reviewsVM = _mapper.Map<IList<ReviewViewModel>>(reviews);
            return View(reviewsVM);
        }

        [HttpPost]
        public async Task<IActionResult> GetComments(Guid bookId)
        {
            var reviews = await _mediator
                .Send(new GetReviewsByBookIdQuery(bookId));

            var reviewsVM = _mapper.Map<IList<ReviewViewModel>>(reviews);
            return PartialView(reviewsVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddReviewCommand model)
        {
            var user = await _mediator
                .Send(new GetUserQuery<string>(User.Identity.Name));

            model.UserId = user.Id;
            var review = await _mediator.Send(model);

            return RedirectToAction("GetBook", "Book", new { review.BookId });
        }

        public async Task<IActionResult> DeleteComment(DeleteReviewCommand model)
        {
            await _mediator.Send(model);
            return RedirectToAction("Index", "Admin");
        }
    }
}
