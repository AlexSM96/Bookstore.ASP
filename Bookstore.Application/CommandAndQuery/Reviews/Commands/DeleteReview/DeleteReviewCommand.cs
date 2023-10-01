namespace Bookstore.Application.CommandAndQuery.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(Guid ReviewId) : IRequest<Unit>; 
}
