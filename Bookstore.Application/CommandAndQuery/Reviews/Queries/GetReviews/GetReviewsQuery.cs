using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Reviews.Queries.GetReviews
{
    public record GetReviewsQuery() : IRequest<IList<Review>>;
}
