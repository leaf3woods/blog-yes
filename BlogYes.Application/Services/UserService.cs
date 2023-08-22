using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.Application.Utilities;
using BlogYes.Core;
using BlogYes.Core.Utilities;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogYes.Application.Services
{
    [Scope("manage all user resources", ManagedResource.User)]
    public class UserService : BaseService, IUserService
    {
        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public async Task<UserReadDto?> RegisterAsync(UserRegisterDto registerDto)
        {
            var user = Mapper.Map<User>(registerDto);
            var count = await _userRepository.CreateAsync(user);
            return count == 0 ? null : Mapper.Map<UserReadDto>(user);
        }

        public async Task<string> LoginAsync(UserLoginDto credential)
        {
            var user = await _userRepository.FindAsync(credential.Username);
            var bytes = Convert.FromBase64String(credential.Password);
            if (user is null || !user.Verify(bytes))
            {
                throw new Exception("user not exist or password error");
            }
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
        public async Task<UserReadDto?> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetQueryWhere(u => u.Id == id).Include(u => u.Role).FirstOrDefaultAsync();
            var result = Mapper.Map<UserReadDto>(user);
            return result;
        }

        [Scope("get all users", ManagedResource.User, ManagedAction.Read, ManagedItem.All)]
        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetQueryWhere().Include(u => u.Role).ToArrayAsync();
            var results = Mapper.Map<IEnumerable<UserReadDto>>(users);
            return results;
        }

        [Scope("change user role", ManagedResource.User, ManagedAction.Update, "Role")]
        public async Task<UserReadDto?> ChangeRole(Guid userId, Guid roleId)
        {
            var user = (await _userRepository.FindAsync(userId)) ??
                throw new Exception("user not find");
            if(await _roleRepository.FindAsync(roleId) is null)
                throw new Exception("role not find");
            user.RoleId = roleId;
            var count = await _userRepository.UpdateAsync(user);
            return count == 0 ? null : Mapper.Map<UserReadDto>(user);
        }
    }
}