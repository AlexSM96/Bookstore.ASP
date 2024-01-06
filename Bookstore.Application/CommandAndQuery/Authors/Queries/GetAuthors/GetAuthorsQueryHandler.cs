namespace Bookstore.Application.CommandAndQuery.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IList<Author>>
    {
        private readonly IBaseDbContext _context;

        public GetAuthorsQueryHandler(IBaseDbContext context) => _context = context;

        public async Task<IList<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken) => 
            await _context.Authors
            .Include(a => a.Books)
            .ToListAsync(cancellationToken);
        
    }
}
