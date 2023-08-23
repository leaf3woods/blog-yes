using BlogYes.Application.Dtos;

namespace BlogYes.Application.Services.Base
{
    public interface IUserService : IBaseService
    {
        public Task<UserReadDto?> RegisterAsync(UserRegisterDto registerDto);

        public Task<string> LoginAsync(UserLoginDto credential);

        public Task<int> DeleteAsync(Guid id);

        public Task<UserReadDto?> GetUserAsync(Guid id);

        public Task<IEnumerable<UserReadDto>> GetUsersAsync();

        public Task<UserReadDto?> ChangeRoleAsync(Guid userId, Guid roleId);

        public Task<CaptchaReadDto> GenerateCaptchaAsync();

    }
}