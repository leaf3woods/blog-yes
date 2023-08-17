using BlogYes.Core;
using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.Repositories;
using BlogYes.Infrastructure.DbContexts;
using BlogYes.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace BlogYes.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot, new()
    {
        public IDbContextFactory<PgDbContext> DbContextFactory { get; set; } = null!;
        private Lazy<PgDbContext> _lazyContext  { get => new(DbContextFactory.CreateDbContext()); }
        public PgDbContext DbContext { get => _lazyContext.Value;}
        public IConfiguration Configuration { get; set; } = null!;

        public async Task<int> CreateAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            var count = await DbContext.SaveChangesAsync();
            return count;
        }

        public async Task<int> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            if(entities.Count() != 0)
            {
                await DbContext.Set<TEntity>().AddRangeAsync(entities);
            }
            var count = await DbContext.SaveChangesAsync();
            return count;
        }

        public IQueryable<TEntity> GetQueryWhere(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null ?
                DbContext.Set<TEntity>() :
                DbContext.Set<TEntity>().Where(expression);
        }

        public async Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            var results = await DbContext.Set<TEntity>()
                .AsNoTracking()
                .ToPaginatedListAsync(pageIndex, pageSize);
            return results;
        }
    }
}