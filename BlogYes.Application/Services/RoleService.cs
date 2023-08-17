using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.Core.Utilities;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using BlogYes.Domain.ValueObjects.UserValue;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Application.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(
            IRoleRepository roleRepository
            )
        {
            _roleRepository = roleRepository;
        }

        private readonly IRoleRepository _roleRepository;

        public async Task<RoleReadDto?> CreateRoleAsync(RoleCreateDto roleDto)
        {
            if (roleDto.Scopes.Any(sc => !SettingUtil.SupportedScopes.Contains(sc.Name)))
            {
                throw new Exception("unsupported scope find");
            }
            var role = Mapper.Map<Role>(roleDto);
            var index = await _roleRepository.CreateAsync(role);
            var dto = Mapper.Map<RoleReadDto>(role);
            return index == 0 ? null : dto;
        }

        public async Task<RoleReadDto?> GetRoleAsync(Guid id)
        {
            var role = await _roleRepository.FindAsync(id);
            var dto = Mapper.Map<RoleReadDto>(role);
            return dto;
        }

        public async Task<IEnumerable<RoleReadDto>> GetRolesAsync()
        {
            var roles =  await _roleRepository.GetQueryWhere().ToArrayAsync();
            var dtos = Mapper.Map<IEnumerable<RoleReadDto>>(roles);
            return dtos;
        }

        public async Task<int> ModifyRoleScopeAsync(Guid roleId, List<RoleScopeModifyDto> scopes)
        {
            if(scopes.Any(sc => !SettingUtil.SupportedScopes.Contains(sc.Name)))
            {
                throw new Exception("unsupported scope find");
            }
            var role = (await _roleRepository.FindAsync(roleId)) ??
                throw new Exception("role is not exist");
            role.Scopes = Mapper.Map<ICollection<Scope>>(scopes);
            return await _roleRepository.UpdateAsync(role);         
        }

        public IEnumerable<RoleScopeReadDto> GetScopes() =>
            SettingUtil.SupportedScopes.Select(sc => new RoleScopeReadDto() { Name = sc});
    }
}
