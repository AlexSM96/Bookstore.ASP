using Category = Bookstore.Domain.Entities.Category;

namespace Bookstore.Application.CommandAndQuery.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Category>
    {
        private readonly IBaseDbContext _context;
        public AddCategoryCommandHandler(IBaseDbContext context) => _context = context;
        public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category();
                if(request is not null)
                {
                    category = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Name == request.Name, cancellationToken) ;

                    if (category is not null)
                    {
                        throw new Exception($"Category {category.Name} is alredy exist");
                    }

                    category = new Category { Name = request.Name };  
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return category;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
