
using BlogYes.Domain.Entities;

namespace BlogYes.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>    
    {
        public Task<int> DeleteAsync(Guid key);

        public Task<User?> FindAsync(Guid key);

        public Task<User?> FindAsync(string username);

        public Task<int> UpdateAsync(User modifiedUser);

    }
}
