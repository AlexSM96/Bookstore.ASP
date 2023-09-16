using Bookstore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Category = Bookstore.Domain.Entities.Category;

namespace Bookstore.Application.CommandAndQuery.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Category>
    {
        private readonly IBaseDbContext _context;

        public AddCategoryCommandHandler(IBaseDbContext context) =>
            _context = context;


        public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == request.Name);

                if (newCategory is not null)
                {
                    throw new Exception($"Category {newCategory.Name} is alredy exist");
                }

                newCategory = new Category { Name = request.Name };

                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync(cancellationToken);

                return newCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
