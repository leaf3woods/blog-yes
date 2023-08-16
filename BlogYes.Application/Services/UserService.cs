using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.Application.Utilities;
using BlogYes.Core;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using System.Security.Claims;

namespace BlogYes.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;

        public async Task<int> RegisterAsync(UserRegisterDto registerDto)
        {
            var user = Mapper.Map<User>(registerDto);
            return await _userRepository.CreateAsync(user);
        }

        public async Task<string> LoginAsync(UserLoginDto credential)
        {
            var user = await _userRepository.FindAsync(credential.Username);
            if (user is null || user.Password != credential.Password)
            {
                throw new Exception("user not exist or password error");
            }
            var token = EncryptUtil.GenerateJwtToken(SettingUtil.Jwt.Issuer, SettingUtil.Jwt.Audience, SettingUtil.Jwt.ExpireMin,
                new Claim(CustomClaimsType.UserId, user.Id.ToString()), new Claim(CustomClaimsType.Role, user.Role.ToString()!)) ??
                throw new Exception("generate jwt token error");
            return token;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserReadDto> GetUserAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var result = Mapper.Map<UserReadDto>(user);
            return result;
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var results = Mapper.Map<IEnumerable<UserReadDto>>(users);
            return results;
        }

    }
}
