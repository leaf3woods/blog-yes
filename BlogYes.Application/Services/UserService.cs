using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.Application.Utilities;
using BlogYes.Core;
using BlogYes.Core.Utilities;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogYes.Application.Services
{
    [Scope("manage all user resources", ManagedResource.User)]
    public class UserService : BaseService, IUserService
    {
        public UserService(
            IUserRepository userRepository
            )
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
            var bytes = Convert.FromBase64String(credential.Password);
            if (user is null || !user.Verify(bytes))
            {
                throw new Exception("user not exist or password error");
            }
            var scopeNames = user.Role.Scopes.Select(x => x.Name);
            var token = CryptoUtil.GenerateJwtToken(SettingUtil.Jwt.Issuer, SettingUtil.Jwt.Audience, SettingUtil.Jwt.ExpireMin,
                new Claim(CustomClaimsType.UserId, user.Id.ToString()), new Claim(CustomClaimsType.RoleId, user.Role.Id.ToString())) ??
                throw new Exception("generate jwt token error");
            return token;
        }

        [Scope("delete user by id", ManagedResource.User, ManagedAction.Delete, ManagedItem.Id)]
        public async Task<int> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        [Scope("get single user by id", ManagedResource.User, ManagedAction.Read, ManagedItem.Id)]
        public async Task<UserReadDto> GetUserAsync(Guid id)
        {
            var user = await _userRepository.FindAsync(id);
            var result = Mapper.Map<UserReadDto>(user);
            return result;
        }

        [Scope("get all users", ManagedResource.User, ManagedAction.Read, ManagedItem.All)]
        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetQueryWhere().ToArrayAsync();
            var results = Mapper.Map<IEnumerable<UserReadDto>>(users);
            return results;
        }

        public async Task<int> ChangeRole(Guid userId, Guid roleId)
        {
            var user = (await _userRepository.FindAsync(userId)) ??
                throw new Exception("user not find");
            if (await _userRepository.FindAsync(roleId) is null)
                throw new Exception("role not find");
            user.RoleId = roleId;
            return await _userRepository.UpdateAsync(user);
        }
    }
}