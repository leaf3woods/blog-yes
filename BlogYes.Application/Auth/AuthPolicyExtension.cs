
using BlogYes.Domain.ValueObjects.UserValue;
using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth
{
    public static class AuthPolicyExtension
    {
        public static void AddPolicyExt(this AuthorizationOptions options, IEnumerable<Scope> scopes) 
        {
            foreach (var scope in scopes)
            {
                options.AddPolicy(scope.Name, policy => policy.AddRequirements())
            }
            
        }

    }
}
