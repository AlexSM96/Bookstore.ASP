using Bookstore.Application.Interfaces;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.CategoryServices
{
    public class CategoryService : IBaseService<Category>
    {
        private readonly IBaseDbContext _context;

        public CategoryService(IBaseDbContext context) =>
            _context = context;

        public async Task<IList<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .ToListAsync(cancellationToken)
                ?? throw new ArgumentNullException();
        }

        public async Task CreateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                var newCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == category.Name);

                if (newCategory is not null)
                {
                    throw new Exception($"Category {newCategory.Name} is alredy exist");
                }

                newCategory = new Category { Name = category.Name };

                await _context.Categories.AddAsync(newCategory);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            try
            {
                var categoryFromDb = await _context.Categories
                .FirstOrDefaultAsync(x => x.Name == category.Name);

                if (categoryFromDb is null)
                {
                    throw new ArgumentNullException($"Category \"{category.Name}\" not found");
                }

                categoryFromDb.Name = category.Name;

                _context.Categories.Update(categoryFromDb);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == id);

                if(category is null)
                {
                    throw new ArgumentNullException($"Category not found [id:{id}]");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
