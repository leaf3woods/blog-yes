using BlogYes.Application.Dtos;

namespace BlogYes.Application.Services.Base
{
    public interface IUserService : IBaseService
    {
        Task<int> RegisterAsync(UserRegisterDto registerDto);
        Task<string> LoginAsync(UserLoginDto credential);
        Task<int> DeleteAsync(Guid id);
        Task<UserReadDto> GetUserAsync(Guid userId);

        Task<IEnumerable<UserReadDto>> GetUsersAsync();
    }
}
