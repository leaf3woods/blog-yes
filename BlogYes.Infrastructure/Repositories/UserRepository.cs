using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public override Task<int> DeleteAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public override Task<int> DeleteRangeAsync<TKey>(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> FindAsync(string username) =>
             await DbContext.Set<User>()
                .SingleOrDefaultAsync(u => u.Username == username);
        

        public override Task<User?> GetAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
