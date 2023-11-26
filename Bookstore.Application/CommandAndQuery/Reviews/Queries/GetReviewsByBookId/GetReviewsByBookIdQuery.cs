namespace Bookstore.Application.CommandAndQuery.Reviews.Queries.GetReviewsByBookId
{
    public record GetReviewsByBookIdQuery(Guid BookId) : IRequest<IList<Review>>;
}
