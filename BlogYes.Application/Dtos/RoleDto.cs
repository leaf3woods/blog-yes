using BlogYes.Application.Dtos.Base;

namespace BlogYes.Application.Dtos
{
    public class RoleCreateDto : CreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IEnumerable<RoleScopeModifyDto> Scopes { get; set; } = null!;
    }

    public class RoleReadDto : ReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IEnumerable<RoleScopeReadDto> Scopes { get; set; } = null!;
    }

    public class RoleScopeReadDto : ReadDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class RoleScopeModifyDto : UpdateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
