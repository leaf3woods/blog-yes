using BlogYes.Application.Dtos;

namespace BlogYes.Application.Services.Base
{
    public interface IRoleService
    {
        public Task<RoleReadDto?> GetRoleAsync(Guid id);

        public Task<IEnumerable<RoleReadDto>> GetRolesAsync();

        public Task<RoleReadDto?> CreateRoleAsync(RoleCreateDto roleDto);

        public Task<int> ModifyRoleScopeAsync(Guid roleId, List<RoleScopeModifyDto> scopeName);

        public IEnumerable<RoleScopeReadDto> GetScopes();
    }
}
