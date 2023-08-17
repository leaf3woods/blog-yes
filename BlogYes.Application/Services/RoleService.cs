using BlogYes.Application.Auth;
using BlogYes.Application.Dtos;
using BlogYes.Application.Services.Base;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogYes.Application.Services
{
    [Scope("manage all role resources", ManagedResource.Role)]
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(
            IRoleRepository roleRepository
            )
        {
            _roleRepository = roleRepository;
        }

        private readonly IRoleRepository _roleRepository;

        [Scope("create a role", ManagedResource.Role, ManagedAction.Create, ManagedItem.Dto)]
        public async Task<RoleReadDto?> CreateRoleAsync(RoleCreateDto roleDto)
        {
            if (RequireScopeUtil.TryFillAll(roleDto.ScopeNames, out var fullScopes))
            {
                throw new Exception("unsupported scope find");
            }
            var role = Mapper.Map<Role>(roleDto);
            role.Scopes = fullScopes;
            var index = await _roleRepository.CreateAsync(role);
            var dto = Mapper.Map<RoleReadDto>(role);
            return index == 0 ? null : dto;
        }

        [Scope("get role info by id", ManagedResource.Role, ManagedAction.Read, ManagedItem.Id)]
        public async Task<RoleReadDto?> GetRoleAsync(Guid id)
        {
            var role = await _roleRepository.FindAsync(id);
            var dto = Mapper.Map<RoleReadDto>(role);
            return dto;
        }

        [Scope("get all roles", ManagedResource.Role, ManagedAction.Read, ManagedItem.All)]
        public async Task<IEnumerable<RoleReadDto>> GetRolesAsync()
        {
            var roles =  await _roleRepository.GetQueryWhere().ToArrayAsync();
            var dtos = Mapper.Map<IEnumerable<RoleReadDto>>(roles);
            return dtos;
        }

        [Scope("change role manage scope", ManagedResource.Role, ManagedAction.Update, "Scopes")]
        public async Task<int> ModifyRoleScopeAsync(Guid roleId, List<string> scopes)
        {
            
            if(RequireScopeUtil.TryFillAll(scopes, out var fullScopes))
            {
                throw new Exception("unsupported scope find");
            }
            var role = (await _roleRepository.FindAsync(roleId)) ??
                throw new Exception("role is not exist");
            role.Scopes = fullScopes;
            return await _roleRepository.UpdateAsync(role);         
        }

        [Scope("get all supported scopes", ManagedResource.Role, ManagedAction.Read, "Scopes")]
        public IEnumerable<RoleScopeReadDto> GetScopes() =>
            RequireScopeUtil.Scopes.Select(Mapper.Map<RoleScopeReadDto>);
    }
}
