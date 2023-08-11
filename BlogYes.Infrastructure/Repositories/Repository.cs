using AutoMapper;
using BlogYes.Core;
using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.Repositories;
using BlogYes.Infrastructure.DbContexts;
using BlogYes.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogYes.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TModel> : IRepository<TEntity, TModel>
        where TEntity : class, IAggregateRoot, new()
        where TModel : class
    {
        public static PooledDbContextFactory<PgDbContext> DbContextFactory { get; set; } = null!;

        private Lazy<PgDbContext> _lazyContext = new Lazy<PgDbContext>(DbContextFactory.CreateDbContext());
        public PgDbContext DbContext { get => _lazyContext.Value;}

        public IMapper Mapper { get; init; } = null!;

        public async Task CreateAsync(TEntity dto)
        {
            var model = Mapper.Map<TEntity, TModel>(dto);
            if(model is not null)
            {
                await DbContext.Set<TModel>().AddAsync(model);
            }
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> dtos)
        {
            var models = Mapper.Map<IEnumerable<TModel>>(dtos);
            if(models.Count() != 0)
            {
                await DbContext.Set<TModel>().AddRangeAsync(models);
            }
        }

        public abstract Task DeleteAsync<TKey>(TKey key);

        public abstract Task DeleteRangeAsync<TKey>(IEnumerable<TKey> ids);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var results = await DbContext.Set<TModel>()
                .AsNoTracking()
                .ToArrayAsync();
            return Mapper.Map<IEnumerable<TEntity>>(results);
        }

        public abstract Task<TEntity?> GetAsync<TKey>(TKey key);

        public async Task<PaginatedList<TEntity>> GetPaginatedAsync(int pageIndex, int pageSize)
        {
            var results = await DbContext.Set<TModel>()
                .AsNoTracking()
                .ToPaginatedListAsync(pageIndex, pageSize);
            return Mapper.Map<PaginatedList<TEntity>>(results);
        }
    }
}