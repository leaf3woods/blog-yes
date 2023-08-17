using BlogYes.Application.Auth.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth.AuthHandler
{
    public class RoleRequireHandler : AuthorizationHandler<RoleRequireScope>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequireScope requirement)
        {
            throw new NotImplementedException();
        }
    }
}
