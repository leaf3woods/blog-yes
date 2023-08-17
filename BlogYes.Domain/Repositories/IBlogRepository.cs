using BlogYes.Domain.Entities;

namespace BlogYes.Domain.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        public Task<int> DeleteAsync(long key);

        public Task<int> DeleteRangeAsync(IEnumerable<long> ids);

        public Task<Blog?> GetAsync(long key);
    }
}