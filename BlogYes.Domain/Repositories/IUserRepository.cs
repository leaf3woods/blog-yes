
using BlogYes.Domain.Entities;

namespace BlogYes.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>    
    {
        public Task<User?> FindAsync(string username);
    }
}
