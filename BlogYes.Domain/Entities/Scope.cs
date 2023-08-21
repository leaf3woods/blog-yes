using BlogYes.Application.Auth;
using BlogYes.Domain.Entities.Base;
using BlogYes.Domain.Utilities;

namespace BlogYes.Domain.Entities
{
    public class Scope : IncrementEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        #region navigation
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;

        #endregion navigation

        public static Scope FromString(string scopeName) => new Scope { Name = scopeName };

        public static Scope[] Seeds { get; } = new Scope[]
        {
            new Scope{ Id = 1, RoleId = Role.AdminRole.Id, Name = ManagedResource.User, Description = RequireScopeUtil.Fill(ManagedResource.User).Description},
            new Scope{ Id = 2, RoleId = Role.SuperRole.Id, Name = ManagedResource.User, Description = RequireScopeUtil.Fill(ManagedResource.User).Description},
            new Scope{ Id = 3, RoleId = Role.SuperRole.Id, Name = ManagedResource.Role, Description = RequireScopeUtil.Fill(ManagedResource.Role).Description},
        };
    }
}