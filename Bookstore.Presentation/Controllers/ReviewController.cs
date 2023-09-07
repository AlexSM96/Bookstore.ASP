using AutoMapper;
using Bookstore.Application.Mapping.ReviewDto;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IBaseService<Review> _service;
        private readonly IMapper _mapper;
        private readonly IBaseDbContext _context;

        public ReviewController(IBaseService<Review> service,
            IMapper mapper, IBaseDbContext context) =>
            (_service, _mapper, _context) = (service, mapper, context);

        [HttpGet]
        public async Task<IActionResult> GetCommentsForAdmin()
        {
            var reviews = await _service
                .GetAllAsync(CancellationToken.None);
            return PartialView(_mapper.Map<IList<ReviewViewModel>>(reviews));
        }

        [HttpPost]
        public async Task<IActionResult> GetComments(Guid bookId)
        {
            var reviews = await _service
                .GetAllAsync(CancellationToken.None);
            var comments = reviews
                .Where(r => r.BookId == bookId)
                .ToList();

            return PartialView(_mapper.Map<IList<ReviewViewModel>>(comments));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(ReviewViewModel model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
            model.UserId = user.Id;
            var review = _mapper.Map<Review>(model);
            await _service.CreateAsync(review, CancellationToken.None);
            return Ok(model);
        }

    }
}
