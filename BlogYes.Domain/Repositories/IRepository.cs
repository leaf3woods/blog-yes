using BlogYes.Domain.Entities.Base;

namespace BlogYes.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
    }
}