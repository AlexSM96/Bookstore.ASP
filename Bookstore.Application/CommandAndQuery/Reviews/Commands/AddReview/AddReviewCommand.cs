using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Reviews.Commands.AddReview
{
    public class AddReviewCommand : IRequest<Review>
    {
        public string Text { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Today;

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }
    }
}
