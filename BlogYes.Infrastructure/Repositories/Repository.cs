using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.Repositories;

namespace BlogYes.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
    }
}