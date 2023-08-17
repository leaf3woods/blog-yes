using Microsoft.AspNetCore.Authorization;

namespace BlogYes.WebApi.Utilities
{
    public class ScopeAuthHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pending = context.Requirements.AsEnumerable();
            context.Succeed()
        }
    }
}
