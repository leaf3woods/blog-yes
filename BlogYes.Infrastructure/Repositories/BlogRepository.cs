using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Infrastructure.Models;

namespace BlogYes.Infrastructure.Repositories
{
    public class BlogRepository : Repository<Blog, BlogDo>, IBlogRepository
    {
        public override Task<int> DeleteAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public override Task<int> DeleteRangeAsync<TKey>(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public override Task<Blog?> GetAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
