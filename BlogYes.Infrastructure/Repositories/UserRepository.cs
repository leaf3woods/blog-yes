using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Infrastructure.Models;

namespace BlogYes.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, UserDo>, IUserRepository
    {
        public override Task<int> DeleteAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public override Task<int> DeleteRangeAsync<TKey>(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public override Task<User?> GetAsync<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
