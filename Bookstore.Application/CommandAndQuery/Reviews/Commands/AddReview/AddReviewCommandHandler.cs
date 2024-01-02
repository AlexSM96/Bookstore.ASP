using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Reviews.Commands.AddReview
{
    internal class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, Review>
    {
        private readonly IBaseDbContext _context;

        public AddReviewCommandHandler(IBaseDbContext context) => _context = context;

        public async Task<Review> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var review = new Review
                {
                    Text = request.Text,
                    BookId = request.BookId,
                    UserId = request.UserId
                };

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync(cancellationToken);

                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
