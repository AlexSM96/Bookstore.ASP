namespace Bookstore.Application.CommandAndQuery.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IList<Category>>
    {
        private readonly IBaseDbContext _context;

        public GetCategoriesQueryHandler(IBaseDbContext context) =>
            _context = context;
        public async Task<IList<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
            => await _context.Categories.ToListAsync(cancellationToken);   
    }
}
