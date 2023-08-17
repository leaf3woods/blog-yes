using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogYes.Infrastructure.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(
                IDbContextFactory<PgDbContext> dbContextFactory,
                IConfiguration configuration
            ) : base(dbContextFactory, configuration) { }

        public async Task<int> DeleteAsync(long key)
        {
            var blog = await DbContext.Set<Blog>()
                .FindAsync(key);
            if (blog is null)
            {
                throw new Exception("user id is not exist!");
            }
            DbContext.Set<Blog>().Remove(blog);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IEnumerable<long> Keys)
        {
            var blogs = DbContext.Set<Blog>()
                .Where(b => Keys.Contains(b.Id));
            DbContext.RemoveRange(Keys);
            return await DbContext.SaveChangesAsync();
        }


        public async Task<Blog?> GetAsync(long key) =>
            await DbContext.Set<Blog>()
                .AsNoTracking()
                .Include(x => x.Comments == null ? null : x.Comments.Take(100))
                .SingleOrDefaultAsync(b => b.Id == key);
    }
}
