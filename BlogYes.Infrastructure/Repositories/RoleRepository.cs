using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public async Task<int> DeleteAsync(Guid key)
        {
            var role = await DbContext.Set<Role>().FindAsync(key);
            if(role is null)
            {
                throw new Exception("role not exist");
            }
            DbContext.Set<Role>().Remove(role);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<Role?> FindAsync(Guid key)
        {
            return await DbContext.Set<Role>().FindAsync(key);
        }

        public async Task<int> UpdateAsync(Role modifiedRole)
        {
            DbContext.Set<Role>().Update(modifiedRole);
            return await DbContext.SaveChangesAsync();
        }
    }
}
