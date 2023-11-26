namespace Bookstore.Application.CommandAndQuery.Reviews.Queries.GetReviews
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, IList<Review>>
    {
        private readonly IBaseDbContext _context;
        public GetReviewsQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<IList<Review>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
            => await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync(cancellationToken);   
    }
}
