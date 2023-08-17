using BlogYes.Domain.Entities;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<int> DeleteAsync(Guid key);

        public Task<Role?> FindAsync(Guid key);

        public Task<int> UpdateAsync(Role modifiedRole);
    }
}
