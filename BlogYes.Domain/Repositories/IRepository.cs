using BlogYes.Core;
using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Repositories
{
    public interface IRepository<TEntity, TModel> 
        where TEntity : class, IAggregateRoot, new()
        where TModel : class
    {
        public Task<TEntity?> GetAsync<TKey>(TKey key);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        public Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize);

        public Task CreateAsync(TEntity dto);

        public Task CreateRangeAsync(IEnumerable<TEntity> dtos);

        public Task DeleteAsync<TKey>(TKey key);

        public Task DeleteRangeAsync<TKey>(IEnumerable<TKey> keys);
    }
}