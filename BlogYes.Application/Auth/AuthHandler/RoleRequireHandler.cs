using BlogYes.Application.Auth.Requirements;
using BlogYes.Application.Services.Base;
using BlogYes.Core;
using Microsoft.AspNetCore.Authorization;

namespace BlogYes.Application.Auth.AuthHandler
{
    public class RoleRequireHandler : AuthorizationHandler<RoleRequireScope>
    {
        public RoleRequireHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        private IRoleService _roleService;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequireScope requirement)
        {
            var dict = context.User.Claims.ToDictionary(key => key.Type, value => value.Value);
            var userId = Guid.Parse(dict[CustomClaimsType.UserId]);
            var scopes = dict[CustomClaimsType.Scopes].Split(',');
            var role = dict[CustomClaimsType.Role];
            if (role == Options.SuperRole || scopes.Any(s => requirement.Scope.StartsWith(s)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}