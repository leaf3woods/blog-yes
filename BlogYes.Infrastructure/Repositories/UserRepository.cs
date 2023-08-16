using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public async Task<int> DeleteAsync(Guid key)
        {
            var user = await DbContext.Set<User>()
                .FindAsync(key);
            if(user is null)
            {
                throw new Exception("user id is not exist!");
            }
            DbContext.Set<User>().Remove(user);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<User?> GetAsync(Guid key) =>
            await DbContext.Set<User>()
                .AsNoTracking()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(u => u.Id == key);

        public async Task<User?> FindAsync(string username) =>
            await DbContext.Set<User>()
                .AsNoTracking()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(u => u.Username == username);
    }
}
