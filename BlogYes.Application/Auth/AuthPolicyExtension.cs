using BlogYes.Application.Auth.Requirements;
using BlogYes.Domain.ValueObjects.UserValue;
using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth
{
    public static class AuthPolicyExtension
    {
        public static void AddPolicyExt(this AuthorizationOptions options, IEnumerable<Scope> scopes) 
        {
            var userScopes = new List<UserRequireScope>();
            var roleScopes = new List<RoleRequireScope>();
            foreach (var scope in scopes)
            {
                var parts = scope.Name.Split('.');
                if (parts.Length == 1)
                {
                    continue;
                }
                switch (parts[0])
                {
                    case ManagedResource.User:
                        var urs = new UserRequireScope(parts[1..]);
                        options.AddPolicy(scope.Name, policy => policy.AddRequirements(urs));
                        userScopes.Add(urs);
                        break;
                    case ManagedResource.Role:
                        var rrs = new RoleRequireScope(parts[1..]);
                        options.AddPolicy(scope.Name, policy => policy.AddRequirements(rrs));
                        roleScopes.Add(rrs);
                        break;
                }
            }
            options.AddPolicy(ManagedResource.User, policy => policy.AddRequirements(userScopes.ToArray()));
            options.AddPolicy(ManagedResource.Role, policy => policy.AddRequirements(roleScopes.ToArray()));
        }

    }
}
