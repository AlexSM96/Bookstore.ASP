namespace Bookstore.Application.CommandAndQuery.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IBaseDbContext _context;
        public DeleteReviewCommandHandler(IBaseDbContext context) => _context = context;
        public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is not null)
                {
                    var reviewFromDb = await _context.Reviews
                        .FindAsync(request.ReviewId, cancellationToken);
                    if (reviewFromDb is null)
                    {
                        throw new ArgumentNullException(nameof(Review));
                    }
                    _context.Reviews.Remove(reviewFromDb);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
