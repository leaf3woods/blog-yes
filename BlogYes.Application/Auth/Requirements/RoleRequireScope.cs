using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth.Requirements
{
    public class RoleRequireScope : IAuthorizationRequirement
    {
        public RoleRequireScope(params string[] scopes)
        {
            Scope = ManagedResource.Role + string.Join('.', scopes);
        }
        public string Scope { get; private set; }
    }
}
