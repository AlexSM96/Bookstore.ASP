namespace Bookstore.Application.CommandAndQuery.Reviews.Queries.GetReviewsByBookId
{
    public class GetReviewsByBookIdQueryHandler : IRequestHandler<GetReviewsByBookIdQuery, IList<Review>>
    {
        private readonly IBaseDbContext _context;
        public GetReviewsByBookIdQueryHandler(IBaseDbContext context) => _context = context;
        public async Task<IList<Review>> Handle(GetReviewsByBookIdQuery request, CancellationToken cancellationToken)
            => request is not null ? await _context.Reviews
                    .Include(x => x.User)
                    .Where(r => r.BookId == request.BookId)
                    .ToListAsync(cancellationToken) : new List<Review>();           
    }
}
