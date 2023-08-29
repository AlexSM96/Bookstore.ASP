namespace Bookstore.Application.Services.Base
{
    public interface IBaseService<T>
    {
        public Task<IList<T>> GetAllAsync(CancellationToken cancellationToken);

        public Task CreateAsync(T model, CancellationToken cancellationToken);

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        public Task UpdateAsync(T model, CancellationToken cancellationToken);
    }
}
