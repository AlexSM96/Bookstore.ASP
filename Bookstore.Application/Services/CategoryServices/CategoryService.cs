using Bookstore.Application.Mapping.CategoryDto;
using Bookstore.Application.Services.Base;

namespace Bookstore.Application.Services.CategoryServices
{
    public class CategoryService : IBaseService<CategoryViewModel>
    {
        public async Task<IList<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CategoryViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task UpdateAsync(CategoryViewModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
