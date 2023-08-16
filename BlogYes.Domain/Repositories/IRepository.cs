using BlogYes.Core;
using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : class, IAggregateRoot, new()
    {
        public Task<int> CreateAsync(TEntity dto);

        public Task<int> CreateRangeAsync(IEnumerable<TEntity> dtos);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        public Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize);
    }
}