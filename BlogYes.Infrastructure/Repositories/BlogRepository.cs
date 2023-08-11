using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Infrastructure.Models;

namespace BlogYes.Infrastructure.Repositories
{
    public class BlogRepository : Repository<Blog, BlogDo>, IBlogRepository
    {
        public override Task DeleteAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteRangeAsync<TKey>(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public override Task<Blog?> GetAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
